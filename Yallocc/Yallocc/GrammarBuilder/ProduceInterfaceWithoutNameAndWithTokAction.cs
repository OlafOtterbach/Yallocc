/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>
using Yallocc.Tokenizer;
using System;

namespace Yallocc
{
   public class ProduceInterfaceWithoutNameAndWithTokAction<TCtx, T> : ProduceInterface<TCtx, T> where T : struct
   {
      public ProduceInterfaceWithoutNameAndWithTokAction(GrammarBuilder<TCtx, T> grammarBuilder)
         : base(grammarBuilder)
      {}

      public ProduceInterface<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterface<TCtx, T>(GrammarBuilder);
      }

      public ProduceInterface<TCtx, T> Action(Action<TCtx, Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
