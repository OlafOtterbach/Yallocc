using System;

namespace Yallocc
{
   public class BranchGosubInterfaceWithoutNameAndWithAction<T> : BranchInterface<T> where T : struct
   {
      public BranchGosubInterfaceWithoutNameAndWithAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterface<T> Action(Action action)
      {
         GrammarBuilder.AddLambda();
         GrammarBuilder.AddAction(action);
         return new BranchInterface<T>(GrammarBuilder);
      }
   }
}
