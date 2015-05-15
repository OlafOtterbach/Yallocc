using System;

namespace ParserLib
{
   public class BranchInterFaceWithoutNameAttribute<T> : BranchInterface<T>
   {
      public BranchInterFaceWithoutNameAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterFaceWithoutNameAndActionAttribute<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
