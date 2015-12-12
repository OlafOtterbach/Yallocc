using Yallocc.Tokenizer;
using System;

namespace Yallocc
{
   public class ProduceInterfaceWithoutNameAndWithTokAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterfaceWithoutNameAndWithTokAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {}

      public ProduceInterface<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterface<T>(GrammarBuilder);
      }

      public ProduceInterface<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterface<T>(GrammarBuilder);
      }
   }
}
