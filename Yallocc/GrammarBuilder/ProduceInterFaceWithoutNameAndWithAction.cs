using System;

namespace Yallocc
{
   public class ProduceInterFaceWithoutNameAndWithAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterFaceWithoutNameAndWithAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProduceInterFaceWithoutNameAndWithoutAction<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterFaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
