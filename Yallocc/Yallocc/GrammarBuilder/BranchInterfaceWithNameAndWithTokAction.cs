using System;

namespace Yallocc
{
   public class BranchInterfaceWithNameAndWithTokAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterfaceWithNameAndWithTokAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public BranchInterfaceWithoutNameAndWithTokAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterfaceWithoutNameAndWithTokAction<T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndWithoutAction<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterfaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndWithoutAction<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterfaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
