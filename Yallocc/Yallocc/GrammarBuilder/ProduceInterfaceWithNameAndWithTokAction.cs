using Yallocc.Tokenizer;
using System;

namespace Yallocc
{
   public class ProduceInterfaceWithNameAndWithTokAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterfaceWithNameAndWithTokAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {
      }

      public ProduceInterfaceWithoutNameAndWithTokAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterfaceWithoutNameAndWithTokAction<T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndWithoutAction<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterfaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndWithoutAction<T> Action(Action<Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterfaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
