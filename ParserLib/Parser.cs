using System.Linq;
using System.Collections.Generic;
using LexSharp;

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
      public bool ParseTokens(Transition start, IEnumerable<Token<T>> sequence)
      {
         var path = new Stack<SyntaxElement>();
         path.Push(new SyntaxElement(start, null));

         SyntaxElement result = null;
         foreach(var token in sequence)
         {
            Lookahead(path, token.Type );
            result = path.Count > 0 ? path.Pop() : null;
            if( result != null)
            {
               path.Clear();
               PushSuccessors(path, result, true);
            }
            else
            {
               break;
            }
         }
         return (result != null) && IsFinished(result);
      }


      private void Lookahead(Stack<SyntaxElement> path, T tokenType)
      {
         while ((path.Count > 0)
                 && (!((path.Peek().Transition is TokenTypeTransition<T>)
                         && ((path.Peek().Transition as TokenTypeTransition<T>).TokenType.Equals(tokenType))
                       )
                    )
               )
         {
            PushSuccessors(path, path.Pop(), false);
         }
      }


      private void PushSuccessors(Stack<SyntaxElement> path, SyntaxElement elem, bool withTokenTypeElement)
      {
         const int max_depth = 10;
         if (path.Count <= max_depth)
         {
            if (elem.Transition is GrammarTransition)
            {
               path.Push(new SyntaxElement((elem.Transition as GrammarTransition).Start, elem));
            }
            else if (elem.Transition is LabelTransition)
            {
               elem.GetSuccessors().ToList().ForEach(e => path.Push(e));
            }
            else if (withTokenTypeElement)
            {
               elem.GetSuccessors().ToList().ForEach(e => path.Push(e));
            }
         }
      }

      private bool IsFinished(SyntaxElement start)
      {
         const int maxElementCount = 10000;

         bool found = !start.GetSuccessors().Any();
         IEnumerable<SyntaxElement> elements = new List<SyntaxElement> { start };
         while ( elements.Any() && (elements.Count() < maxElementCount) && (!found) )
         {
            elements = elements.Where(x => !(x.Transition is TokenTypeTransition<T>)).SelectMany(e => e.GetSuccessors());
            found = elements.Any(x => !x.GetSuccessors().Any());
         }

         return found;
      }
   }
}
