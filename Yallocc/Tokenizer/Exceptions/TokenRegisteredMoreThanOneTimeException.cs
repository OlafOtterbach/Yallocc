/// <summary>Tokenizer</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

using System;

namespace Yallocc.Tokenizer
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
