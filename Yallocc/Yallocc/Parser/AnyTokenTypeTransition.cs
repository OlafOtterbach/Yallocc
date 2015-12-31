using System;

namespace Yallocc
{
   public class AnyTokenTypeTransition<TCtx, T> : TokenTypeTransition<TCtx, T> where T : struct
   {
      public AnyTokenTypeTransition() : base(default(T))
      {}

      public override bool HasMatchingTokenType(Nullable<T> tokenType)
      {
         return true;
      }
   }
}