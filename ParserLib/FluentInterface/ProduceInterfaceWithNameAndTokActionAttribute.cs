using LexSharp;
using System;

namespace ParserLib
{
   public class ProduceInterfaceWithNameAndTokActionAttribute<T> : ProduceInterface<T>
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
