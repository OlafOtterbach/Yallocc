using System;

namespace ParserLib
{
   public class ProducerInterfaceWithNameAndActionAttribute<T> : ProducerInterface<T>
   {
      public ProducerInterfaceWithNameAndActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public ProducerInterFaceWithoutNameAttribute<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProducerInterFaceWithoutNameAttribute<T>(GrammarBuilder);
      }

      public ProducerInterFaceWithoutActionAttribute<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ProducerInterFaceWithoutActionAttribute<T>(GrammarBuilder);
      }
   }
}
