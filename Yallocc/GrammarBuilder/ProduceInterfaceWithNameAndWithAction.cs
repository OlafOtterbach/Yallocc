using System;

namespace Yallocc
{
   public class ProduceInterfaceWithNameAndWithAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterfaceWithNameAndWithAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public ProduceInterFaceWithoutNameAndWithAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterFaceWithoutNameAndWithAction<T>(GrammarBuilder);
      }

      public ProduceInterFaceWithNameAndWithoutAction<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterFaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
