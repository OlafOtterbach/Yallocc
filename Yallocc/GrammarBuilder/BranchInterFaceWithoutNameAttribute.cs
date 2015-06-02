﻿using System;

namespace Yallocc
{
   public class BranchInterFaceWithoutNameAttribute<T> : BranchInterface<T> where T : struct
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
