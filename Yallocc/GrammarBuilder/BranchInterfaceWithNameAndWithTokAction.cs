using LexSharp;
using System;

namespace Yallocc
{
   public class BranchInterfaceWithNameAndWithTokAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterfaceWithNameAndWithTokAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public BranchInterFaceWithoutNameWithTokActionAttribute<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterFaceWithoutNameWithTokActionAttribute<T>(GrammarBuilder);
      }

      public BranchInterFaceWithoutActionAttribute<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterFaceWithoutActionAttribute<T>(GrammarBuilder);
      }
   }
}
