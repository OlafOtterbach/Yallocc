/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>
using System;

namespace Yallocc
{
   public class ProduceInterfaceWithoutNameAndWithAction<TCtx, T> : ProduceInterface<TCtx, T> where T : struct
   {
      public ProduceInterfaceWithoutNameAndWithAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProduceInterface<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
