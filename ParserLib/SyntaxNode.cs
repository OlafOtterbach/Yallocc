using System.Collections.Generic;

namespace ParserLib
{
   internal class SyntaxNode<T>
   {
      public SyntaxNode(T tokenType)
      {
         TokenType = tokenType;
      }

      public T TokenType { get; private set; }
   }
}
