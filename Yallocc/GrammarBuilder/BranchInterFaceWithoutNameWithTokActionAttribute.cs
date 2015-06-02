using LexSharp;
using System;

namespace Yallocc
{
   public class BranchInterFaceWithoutNameWithTokActionAttribute<T> : BranchInterface<T> where T : struct
   {
      public BranchInterFaceWithoutNameWithTokActionAttribute(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {}

      public BranchInterFaceWithoutNameAndActionAttribute<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
