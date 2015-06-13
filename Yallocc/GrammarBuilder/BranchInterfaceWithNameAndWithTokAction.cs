using LexSharp;
using System;

namespace Yallocc
{
   public class BranchInterfaceWithNameAndWithTokAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterfaceWithNameAndWithTokAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public BranchInterFaceWithoutNameAndWithTokAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterFaceWithoutNameAndWithTokAction<T>(GrammarBuilder);
      }

      public BranchInterFaceWithNameAndWithoutAction<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterFaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
