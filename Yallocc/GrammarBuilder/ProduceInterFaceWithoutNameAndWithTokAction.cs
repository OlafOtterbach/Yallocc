using LexSharp;
using System;

namespace Yallocc
{
   public class ProduceInterFaceWithoutNameAndWithTokAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterFaceWithoutNameAndWithTokAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {}

      public ProduceInterFaceWithoutNameAndWithoutAction<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterFaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
