using LexSharp;
using System;

namespace Yallocc
{
   public class BranchInterFaceWithoutNameAndWithTokAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterFaceWithoutNameAndWithTokAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {}

      public BranchInterFaceWithoutNameAndWithoutAction<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterFaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
