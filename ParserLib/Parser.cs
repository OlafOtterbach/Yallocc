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
         var path = new Stack<SyntaxElement>();
         path.Push(new SyntaxElement(start, null));

         SyntaxElement result = null;
         foreach(var token in sequence)
         {//!!!!!!!!!!
            Lookahead(result, path, token.Type, 0 );
            result = path.Count > 0 ? path.Peek() : null;
            if( result != null)
            {
               path.ToList().ForEach(x => x.Transition.Execute());
               path.Clear();
               PushSuccessors(path, result, true);
            }
            else
            {
               break;
            }
         }
         return (result != null) && IsFinished(result,0);
      }

      private bool Lookahead(SyntaxElement start, Stack<SyntaxElement> path, T tokenType, int counter)
      {
         var enumerator = GetSuccessors(start).GetEnumerator();
         var found = false;
         while((!found) && (counter < Max ) && enumerator.MoveNext() )
         {
            var succ = enumerator.Current;
            if((succ.Transition is TokenTypeTransition<T>))
            {
               if((succ.Transition as TokenTypeTransition<T>).TokenType.Equals(tokenType))
               {
                  path.Push(succ);
                  found = true;
               }
            }
            else
            {
               path.Push(succ);
               found = Lookahead(succ,path,tokenType,counter++);
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


      private void Lookahead2(Stack<SyntaxElement> path, T tokenType)
      {
         while (    (path.Count > 0)
                 && (!      ((path.Peek().Transition is TokenTypeTransition<T>)
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
            else if (withTokenTypeElement && (elem.Transition is TokenTypeTransition<T>))
            {
               elem.GetSuccessors().ToList().ForEach(e => path.Push(e));
            }
         }
      }

      private bool IsFinished(SyntaxElement start)
      {
         var path = new Stack<SyntaxElement>();
         path.Push(start);
         var result = SearchForEmptyPath(path);
         return result;
      }

      private bool SearchForEmptyPath(Stack<SyntaxElement> path)
      {
         bool found = false;
         while((!found) && (path.Count > 0))
         {
            found = PushNoTokenSuccessors(path, path.Pop());
         }
         return found;
      }

      private bool PushNoTokenSuccessors(Stack<SyntaxElement> path, SyntaxElement elem)
      {
         if(!elem.GetSuccessors().Any())
         {
            return true;
         }
         const int max_depth = 10;
         if (path.Count <= max_depth)
         {
            var count = path.Count;
            if (elem.Transition is GrammarTransition)
            {
               var successor = (elem.Transition as GrammarTransition).Start;
               if (!(successor is TokenTypeTransition<T>))
               {
                  path.Push(new SyntaxElement((elem.Transition as GrammarTransition).Start, elem));
               }
            }
            else
            {
               elem.GetSuccessors().Where(x => !(x.Transition is TokenTypeTransition<T>)).ToList().ForEach(e => path.Push(e));
            }
         }
         return false;
      }
   }
}
