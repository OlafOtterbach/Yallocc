using Yallocc.Tokenizer;
using System;

namespace Yallocc
{
   public class TokenTypeTransition<TCtx, T> : Transition where T : struct
   {
      public TokenTypeTransition( T tokenType ) : base()
      {
         TokenType = tokenType;
         Action = (TCtx ctx, Token<T> token) => {};
      }

      public T TokenType { get; }

      public Action<TCtx, Token<T>> Action { get; set; }
   }
}
