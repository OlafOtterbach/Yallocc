using LexSharp;
using System.Collections.Generic;
using System.Linq;

namespace Yallocc
{
   public class Parser<T>
   {
      public Parser()
      {
         Max = 100;
      }

      public int Max { get; set; }

      public ParserResult ParseTokens(Transition start, IEnumerable<Token<T>> sequence)
      {
         ParserResult result = new ParserResult();
         SyntaxElement elem = new SyntaxElement(start,null);
         var path = new List<SyntaxElement>();
         int index = 0;
         foreach (var token in sequence)
         {
            if (Lookahead(elem, path, token.Type, 0, index++ == 0))
            {
               result.Position = token.TextIndex;
               elem = path.Last();
               path.ToList().ForEach(x => Execute(x.Transition, token));
               path.Clear();
            }
            else
            {
               result.SyntaxError = true;
               result.Position = token.TextIndex;
               break;
            }
         }

         var endPath = new List<SyntaxElement>();
         var isFinished = result.Success && IsFinished(elem,0, endPath);
         if(isFinished)
         {
            endPath.Select(x => x.Transition).OfType<ActionTransition>().ToList().ForEach(t => t.Action());
         }
         else
         {
            result.GrammarOfTextNotComplete = result.Success;
         }

         return result;
      }

      private void Execute(Transition transition, Token<T> token)
      {
         if(transition is ActionTransition)
         {
            (transition as ActionTransition).Action();
         }
         else if (transition is TokenTypeTransition<T>)
         {
            (transition as TokenTypeTransition<T>).Action(token);
         }
      }

      private bool Lookahead(SyntaxElement start, List<SyntaxElement> path, T tokenType, int counter, bool first)
      {
         var found = false;
         var enumerator = first ? new List<SyntaxElement>{start}.GetEnumerator() : GetSuccessors(start).GetEnumerator();
         while ((counter < Max) && (!found) && enumerator.MoveNext())
         {
            var succ = enumerator.Current;
            if (!(succ.Transition is TokenTypeTransition<T>) || ((succ.Transition as TokenTypeTransition<T>).TokenType.Equals(tokenType)))
            {
               path.Add(succ);
               found = (succ.Transition is TokenTypeTransition<T>) || Lookahead(succ, path, tokenType, ++counter,false);
            }
         }
         return found;
      }

      private bool IsFinished(SyntaxElement start, int counter, List<SyntaxElement> path)
      {
         var successors = GetSuccessors(start);
         var enumerator = successors.GetEnumerator();
         var found = !successors.Any();
         while ((!found) && (counter < Max) && enumerator.MoveNext())
         {
            var succ = enumerator.Current;
            found = (!(succ.Transition is TokenTypeTransition<T>)) && IsFinished(succ, counter++, path);
            if(found)
            {
               path.Add(succ);
            }
         }
         return found;
      }

      private IEnumerable<SyntaxElement> GetSuccessors(SyntaxElement elem)
      {
         if (elem.Transition is GrammarTransition)
         {
            return new List<SyntaxElement>(){new SyntaxElement((elem.Transition as GrammarTransition).Start, elem)};
         }
         else
         {
            return elem.GetSuccessors();
         }
      }
   }
}
