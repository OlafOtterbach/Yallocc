using System;

namespace Yallocc
{
   public class ProduceInterfaceWithNameAndWithAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterfaceWithNameAndWithAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public ProduceInterfaceWithoutNameAndWithAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterfaceWithoutNameAndWithAction<T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndWithoutAction<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterfaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
