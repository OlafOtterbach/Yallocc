using System;

namespace Yallocc
{
   public class BranchInterfaceWithNameAndWithAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterfaceWithNameAndWithAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public BranchInterFaceWithoutNameAndWithAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterFaceWithoutNameAndWithAction<T>(GrammarBuilder);
      }

      public BranchInterFaceWithNameAndWithoutAction<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterFaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
