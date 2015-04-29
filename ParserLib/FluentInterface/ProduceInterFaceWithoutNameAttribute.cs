using System;

namespace ParserLib
{
   public class ProducerInterFaceWithoutNameAttribute<T> : ProducerInterface<T>
   {
      public ProducerInterFaceWithoutNameAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProducerInterFaceWithoutNameAndActionAttribute<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ProducerInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
