using System.Linq;
using System.Collections.Generic;
using LexSharp;

namespace ParserLib
{
   internal class TransistionStackElem
   {
      private TransistionStackElem _parentContext;

      public TransistionStackElem(Transition transition, TransistionStackElem parentContext)
      {
         _parentContext = parentContext;
         Transition = transition;
      }

      public Transition Transition { get; private set; }

      public IEnumerable<TransistionStackElem> GetSuccessors()
      {
         var successors = Transition.Successors.Any()
            ? Transition.Successors.Select( t => new TransistionStackElem(t, _parentContext))
            : _parentContext == null
               ? _parentContext.GetSuccessors()
               : new List<TransistionStackElem>();
         return successors;
      }
   }


   public class Parser<T>
   {
      Stack<TransistionStackElem> _path;

      public Parser()
      {
         _path = new Stack<TransistionStackElem>();
      }


      public void ParseTokens(IEnumerable<Token<T>> sequence)
      {
//         var parseResult = sequence.Select( t => Lookahead(t)).TakeWhile(r => r.Success);
         //ParseResult result = new ParseResult();
         //foreach(var token in sequence)
         //{
         //   var res = Lookahead(token);
         //   if(res != ParseResultType.Success)
         //   {
         //      result.ResultType = res;
         //      result.Token = token;
         //      break;
         //   }
         //}
      }

      public void LookAhead(T tokenType)
      {
         while(_path.Count > 0)
         {
            var elem = _path.Pop();
            if( (elem.Transition is TokenTypeTransition<T>) && ((elem.Transition as TokenTypeTransition<T>).TokenType.Equals(tokenType))
            {
               // Hier noch Ergebnisrückgabe
               return;
            }
            else if (elem.Transition is GrammarTransition)
            {
               _path.Push(new TransistionStackElem((elem.Transition as GrammarTransition).Grammar.StartTransition, elem));
            }
            else
            {
               elem.GetSuccessors().ToList().ForEach(e => _path.Push(e));
            }
         }
      }
   }
}
