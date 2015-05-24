﻿using System;

namespace Yallocc
{
   public class ProduceInterFaceWithoutNameAttribute<T> : ProduceInterface<T>
   {
      public ProduceInterFaceWithoutNameAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProduceInterFaceWithoutNameAndActionAttribute<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}