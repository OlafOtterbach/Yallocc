using System;

namespace Yallocc
{
   public class BranchDefaultInterfaceWithoutNameAndWithAction<T> : BranchBuilder<T> where T : struct
   {
      public BranchDefaultInterfaceWithoutNameAndWithAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      { }

      public BranchDefaultInterfaceWithoutNameAndWithoutAction<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchDefaultInterfaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
