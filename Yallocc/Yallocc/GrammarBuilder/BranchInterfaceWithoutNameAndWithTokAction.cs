using Yallocc.Tokenizer;
using System;

namespace Yallocc
{
   public class BranchInterfaceWithoutNameAndWithTokAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterfaceWithoutNameAndWithTokAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {}

      public BranchInterface<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterface<T>(GrammarBuilder);
      }

      public BranchInterface<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterface<T>(GrammarBuilder);
      }
   }
}
