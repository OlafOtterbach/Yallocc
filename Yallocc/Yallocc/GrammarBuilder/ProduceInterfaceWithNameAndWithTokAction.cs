/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>
using Yallocc.Tokenizer;
using System;

namespace Yallocc
{
   public class ProduceInterfaceWithNameAndWithTokAction<TCtx, T> : ProduceInterface<TCtx, T> where T : struct
   {
      public ProduceInterfaceWithNameAndWithTokAction(GrammarBuilder<TCtx, T> grammarBuilder)
         : base(grammarBuilder)
      {
      }

      public ProduceInterfaceWithoutNameAndWithTokAction<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterfaceWithoutNameAndWithTokAction<TCtx, T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndWithoutAction<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterfaceWithNameAndWithoutAction<TCtx, T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndWithoutAction<TCtx, T> Action(Action<TCtx, Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterfaceWithNameAndWithoutAction<TCtx, T>(GrammarBuilder);
      }
   }
}
