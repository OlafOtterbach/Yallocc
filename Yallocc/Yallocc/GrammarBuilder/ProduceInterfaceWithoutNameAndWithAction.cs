using System;

namespace Yallocc
{
   public class ProduceInterfaceWithoutNameAndWithAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterfaceWithoutNameAndWithAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProduceInterface<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterface<T>(GrammarBuilder);
      }
   }
}
