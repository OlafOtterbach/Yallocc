using LexSharp;
using System;

namespace Yallocc
{
   public class ProduceInterFaceWithoutNameWithTokActionAttribute<T> : ProduceInterface<T>
   {
      public ProduceInterFaceWithoutNameWithTokActionAttribute(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {}

      public ProduceInterFaceWithoutNameAndActionAttribute<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
