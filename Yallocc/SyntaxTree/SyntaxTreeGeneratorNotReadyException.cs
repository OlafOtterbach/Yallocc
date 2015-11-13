using System;

namespace SyntaxTree
{
   public class SyntaxTreeGeneratorNotReadyException : Exception
   {
      public SyntaxTreeGeneratorNotReadyException()
      {
      }

      public SyntaxTreeGeneratorNotReadyException(string message)
         : base(message)
      {
      }

      public SyntaxTreeGeneratorNotReadyException(string message, Exception inner)
         : base(message, inner)
      {
      }
   }
}
