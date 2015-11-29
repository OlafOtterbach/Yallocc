using System;

namespace Yallocc.Tokenizer
{
   public class TokenIsNotAnEnumTypeException : Exception
   {
      public TokenIsNotAnEnumTypeException()
      {
      }

      public TokenIsNotAnEnumTypeException(string message)
         : base(message)
      {
      }

      public TokenIsNotAnEnumTypeException(string message, Exception inner)
         : base(message, inner)
      {
      }
   }
}
