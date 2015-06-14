using System;

namespace Yallocc
{
   public class BranchInterFaceWithoutNameAndWithAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterFaceWithoutNameAndWithAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterface<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterface<T>(GrammarBuilder);
      }
   }
}
