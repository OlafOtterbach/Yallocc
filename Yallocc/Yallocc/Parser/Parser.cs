﻿/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

using System;
using System.Collections.Generic;
using System.Linq;
using Yallocc.Tokenizer;

namespace Yallocc
{
   public class Parser<TCtx, T> where T : struct
   {
      private readonly Transition _masterGrammarStartTransition;

      private readonly int _maxLookaheadSearchDepth;


      public Parser(Transition masterGrammar, int max = 100 )
      {
         _masterGrammarStartTransition = masterGrammar;
         _maxLookaheadSearchDepth = max;
      }


      public ParserResult<T> ParseTokens(IEnumerable<Token<T>> sequence, TCtx context)
      {
         ParserResult<T> result = new ParserResult<T>();
         SyntaxElement elem = new SyntaxElement(_masterGrammarStartTransition, null);
         var path = new Stack<SyntaxElement>();
         int index = 0;
         foreach (var token in sequence)
         {
            if (Lookahead(elem, path, token.Type, 0, index++ == 0))
            {
               result.Position = token.TextIndex;
               elem = path.Peek();
               path.Reverse().ToList().ForEach(x => Execute(x.Transition, token, context));
               path.Clear();
            }
            else
            {
               result.SyntaxError = true;
               result.Position = token.TextIndex;
               result.FailedToken = token;
               break;
            }
         }

         var endPath = new List<SyntaxElement>();
         var isFinished = result.Success && IsFinished(elem, 0, endPath);
         if (isFinished)
         {
            endPath.Select(x => x.Transition)
                   .OfType<ActionTransition<TCtx>>()
                   .ToList()
                   .ForEach(t => t.Action(context));
         }
         else
         {
            result.GrammarOfTextNotComplete = result.Success;
         }

         return result;
      }

      private void Execute(Transition transition, Token<T> token, TCtx context)
      {
         if (transition is ActionTransition<TCtx>)
         {
            (transition as ActionTransition<TCtx>).Action(context);
         }
         else if (transition is TokenTypeTransition<TCtx, T>)
         {
            (transition as TokenTypeTransition<TCtx, T>).Action(context, token);
         }
      }

      private bool Lookahead(SyntaxElement start, Stack<SyntaxElement> path, Nullable<T> tokenType, int counter, bool first)
      {
         var found = false;
         var enumerator = first ? new List<SyntaxElement> { start }.GetEnumerator() : GetSuccessors(start).GetEnumerator();
         while ((counter < _maxLookaheadSearchDepth) && (!found) && enumerator.MoveNext())
         {
            var succ = enumerator.Current;
            if (!(succ.Transition is TokenTypeTransition<TCtx,T>) || ((succ.Transition as TokenTypeTransition<TCtx,T>).HasMatchingTokenType(tokenType)))
            {
               path.Push(succ);
               if ((succ.Transition is TokenTypeTransition<TCtx,T>) || Lookahead(succ, path, tokenType, ++counter, false))
               {
                  found = true;
               }
               else
               {
                  path.Pop();
               }
            }
         }
         return found;
      }

      private bool IsFinished(SyntaxElement start, int counter, List<SyntaxElement> path)
      {
         var successors = GetSuccessors(start);
         var enumerator = successors.GetEnumerator();
         var found = !successors.Any();
         while ((!found) && (counter < _maxLookaheadSearchDepth) && enumerator.MoveNext())
         {
            var succ = enumerator.Current;
            found = (!(succ.Transition is TokenTypeTransition<TCtx,T>)) && IsFinished(succ, counter++, path);
            if (found)
            {
               path.Insert(0, succ);
            }
         }
         return found;
      }

      private IEnumerable<SyntaxElement> GetSuccessors(SyntaxElement elem)
      {
         if (elem.Transition is GrammarTransition<TCtx>)
         {
            return new List<SyntaxElement>() { new SyntaxElement((elem.Transition as GrammarTransition<TCtx>).Start, elem) };
         }
         else
         {
            return elem.GetSuccessors();
         }
      }
   }
}
