using LexSharp;
using System;

namespace Yallocc
{
   public class ProduceInterfaceWithNameAndTokActionAttribute<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterfaceWithNameAndTokActionAttribute(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {
      }

      public ProduceInterFaceWithoutNameWithTokActionAttribute<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterFaceWithoutNameWithTokActionAttribute<T>(GrammarBuilder);
      }

      public ProduceInterFaceWithoutActionAttribute<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterFaceWithoutActionAttribute<T>(GrammarBuilder);
      }
   }
}
