using System;

namespace Yallocc
{
   public class BranchDefaultInterfaceWithNameAndWithAction<T> : BranchBuilder<T> where T : struct
   {
      public BranchDefaultInterfaceWithNameAndWithAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {
      }

      public BranchDefaultInterfaceWithoutNameAndWithAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchDefaultInterfaceWithoutNameAndWithAction<T>(GrammarBuilder);
      }

      public BranchDefaultInterfaceWithNameAndWithoutAction<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchDefaultInterfaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
