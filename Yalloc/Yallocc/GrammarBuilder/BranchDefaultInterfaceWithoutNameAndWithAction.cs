using System;

namespace Yallocc
{
   public class BranchDefaultInterfaceWithoutNameAndWithAction<T> : BranchBuilder<T> where T : struct
   {
      public BranchDefaultInterfaceWithoutNameAndWithAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      { }

      public BranchBuilder<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchBuilder<T>(GrammarBuilder);
      }
   }
}
