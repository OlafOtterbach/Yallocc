using System.Linq;
using System.Collections.Generic;
using LexSharp;
using System;

namespace ParserLib
{
   internal class SyntaxElement
   {
      private SyntaxElement _parentContext;

      public SyntaxElement(Transition transition, SyntaxElement parentContext)
      {
         _parentContext = parentContext;
         Transition = transition;
      }

      public Transition Transition { get; private set; }

      public IEnumerable<SyntaxElement> GetSuccessors()
      {
         var successors = Transition.Successors.Any()
            ? Transition.Successors.Select( t => new SyntaxElement(t, _parentContext))
            : _parentContext != null
               ? _parentContext.GetSuccessors()
               : new List<SyntaxElement>();
         return successors;
      }
   }


   public class Parser<T>
   {
      public Parser()
      {
         Max = 100;
      }

      public int Max { get; set; }

      public bool ParseTokens(Transition start, IEnumerable<Token<T>> sequence)
      {
         bool result = true;
         SyntaxElement elem = new SyntaxElement(start,null);
         var path = new List<SyntaxElement>();
         int index = 0;
         foreach (var token in sequence)
         {
            if (Lookahead(elem, path, token.Type, 0, index++ == 0))
            {
               elem = path.Last();
               path.ToList().ForEach(x => x.Transition.Execute());
               path.Clear();
            }
            else
            {
               result = false;
               break;
            }
         }
         return result && IsFinished(elem,0);
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
               found = (succ.Transition is TokenTypeTransition<T>) || Lookahead(succ, path, tokenType, counter++,false);
            }
         }
         return found;
      }

      private bool IsFinished(SyntaxElement start, int counter)
      {
         var successors = GetSuccessors(start);
         var enumerator = successors.GetEnumerator();
         var found = !successors.Any();
         while ((!found) && (counter < Max) && enumerator.MoveNext())
         {
            var succ = enumerator.Current;
            found = (!(succ.Transition is TokenTypeTransition<T>)) && IsFinished(succ, counter++);
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
