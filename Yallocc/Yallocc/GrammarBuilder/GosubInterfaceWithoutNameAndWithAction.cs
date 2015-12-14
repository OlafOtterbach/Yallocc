using System;

namespace Yallocc
{
   public class  GosubInterfaceWithoutNameAndWithAction<TCtx,T> : ProduceInterface<TCtx, T> where T : struct
   {
      public GosubInterfaceWithoutNameAndWithAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProduceInterface<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddLambda();
         GrammarBuilder.AddAction(action);
         return new ProduceInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
