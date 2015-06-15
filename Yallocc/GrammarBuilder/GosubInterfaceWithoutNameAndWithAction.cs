using System;

namespace Yallocc
{
   public class  GosubInterfaceWithoutNameAndWithAction<T> : ProduceInterface<T> where T : struct
   {
      public GosubInterfaceWithoutNameAndWithAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProduceInterface<T> Action(Action action)
      {
         GrammarBuilder.AddLambda();
         GrammarBuilder.AddAction(action);
         return new ProduceInterface<T>(GrammarBuilder);
      }
   }
}
