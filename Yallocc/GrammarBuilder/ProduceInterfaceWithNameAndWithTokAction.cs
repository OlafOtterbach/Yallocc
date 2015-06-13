using LexSharp;
using System;

namespace Yallocc
{
   public class ProduceInterfaceWithNameAndWithTokAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterfaceWithNameAndWithTokAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {
      }

      public ProduceInterFaceWithoutNameAndWithTokAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterFaceWithoutNameAndWithTokAction<T>(GrammarBuilder);
      }

      public ProduceInterFaceWithNameAndWithoutAction<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterFaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
