using System;

namespace Yallocc
{
   public class BranchInterfaceWithNameAndWithAction<TCtx, T> : BranchInterface<TCtx, T> where T : struct
   {
      public BranchInterfaceWithNameAndWithAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public BranchInterFaceWithoutNameAndWithAction<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterFaceWithoutNameAndWithAction<TCtx, T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndWithoutAction<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterfaceWithNameAndWithoutAction<TCtx, T>(GrammarBuilder);
      }
   }
}
