using LexSharp;
using System;

namespace Yallocc
{
   public class TokenTypeTransition<T> : Transition
   {
      public TokenTypeTransition( T tokenType ) : base()
      {
         TokenType = tokenType;
         Action = (Token<T> token) => {};
      }

      public T TokenType { get; private set; }

      public Action<Token<T>> Action { get; set; }
   }
}
