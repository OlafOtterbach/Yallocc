using System;

namespace LexSharp
{
   public class TokenRegisteredMoreThanOneTimeException<T> : Exception
   {
      public TokenRegisteredMoreThanOneTimeException()
      {
      }

      public TokenRegisteredMoreThanOneTimeException(string message)
         : base(message)
      {
      }

      public TokenRegisteredMoreThanOneTimeException(T tokenType, string message)
         : base(message)
      {
         TokenType = tokenType;
      }

      public TokenRegisteredMoreThanOneTimeException(string message, Exception inner)
         : base(message, inner)
      {
      }

      public TokenRegisteredMoreThanOneTimeException(T tokenType, string message, Exception inner)
         : base(message, inner)
      {
         TokenType = tokenType;
      }

      public T TokenType { get; set; }
   }
}
