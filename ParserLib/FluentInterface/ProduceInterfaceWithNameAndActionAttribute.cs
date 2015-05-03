using System;

namespace ParserLib
{
   public class ProduceInterfaceWithNameAndActionAttribute<T> : ProduceInterface<T>
   {
      public ProduceInterfaceWithNameAndActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public ProduceInterFaceWithoutNameAttribute<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterFaceWithoutNameAttribute<T>(GrammarBuilder);
      }

      public ProduceInterFaceWithoutActionAttribute<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterFaceWithoutActionAttribute<T>(GrammarBuilder);
      }
   }
}
