using System.Linq;
using System.Collections.Generic;
using LexSharp;

namespace ParserLib
{
   public class Parser<T>
   {
      Stack<Transition> _path;

      public Parser()
      {
         _path = new Stack<Transition>();
      }

      public bool Parse(Transition startTransition, List<Token<T>> sequence, int index)
      {
         _path.Push(startTransition);
         while(_path.Count > 0)
         {
            if (index >= sequence.Count)
            {
               return false;
            }

            var trans = _path.Pop();
            if(trans is SyntaxNodeTransition<T>)
            {
               var syntaxNode = trans as SyntaxNode<T>;
               if(syntaxNode.TokenType.Equals(sequence[index].Type))
               {
                  _path.Clear();
                  index++;
                  if(index == sequence.Count)
                  {
                     return true;
                  }
               }
               else
               {
                  trans = new Transition(); // Dummytransition
               }
            }
            else if( trans is GrammarTransition)
            {
               var grammerTransition = trans as GrammarTransition;
               Parse(grammerTransition.Grammar.StartTransition, sequence, index);
            }
            trans.Successors.ToList().ForEach(x => _path.Push(x));
         }
         return true;
      }

      public void Lookahead(Transition start, T tokenType)
      {
         start.Successors.ToList().ForEach(t => _path.Push(t));
         while (_path.Count > 0)
         {
            var transition = _path.Pop();
            if((transition is SyntaxNodeTransition<T>))
            {
               var token = (transition as SyntaxNodeTransition<T>). 
            }
         }

      }
   }
}
