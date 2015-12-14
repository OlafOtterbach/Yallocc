using System;

namespace Yallocc
{
   public class BranchGosubInterfaceWithoutNameAndWithAction<TCtx, T> : BranchInterface<TCtx, T> where T : struct
   {
      public BranchGosubInterfaceWithoutNameAndWithAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterface<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddLambda();
         GrammarBuilder.AddAction(action);
         return new BranchInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
