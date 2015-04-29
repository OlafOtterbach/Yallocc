﻿using System;

namespace ParserLib
{
   public class BranchInterfaceWithNameAndActionAttribute<T> : BranchInterface<T>
   {
      public BranchInterfaceWithNameAndActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public BranchInterFaceWithoutNameAttribute<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterFaceWithoutNameAttribute<T>(GrammarBuilder);
      }

      public BranchInterFaceWithoutActionAttribute<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterFaceWithoutActionAttribute<T>(GrammarBuilder);
      }
   }
}
