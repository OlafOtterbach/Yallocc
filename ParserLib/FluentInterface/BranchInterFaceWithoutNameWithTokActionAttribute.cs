using LexSharp;
using System;

namespace ParserLib
{
   public class BranchInterFaceWithoutNameWithTokActionAttribute<T> : BranchInterface<T>
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
