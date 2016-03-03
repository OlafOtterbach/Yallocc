/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>
using System;

namespace Yallocc
{
   public class ProduceInterfaceWithNameAndWithAction<TCtx,T> : ProduceInterface<TCtx, T> where T : struct
   {
      public ProduceInterfaceWithNameAndWithAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public ProduceInterfaceWithoutNameAndWithAction<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterfaceWithoutNameAndWithAction<TCtx, T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndWithoutAction<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new ProduceInterfaceWithNameAndWithoutAction<TCtx, T>(GrammarBuilder);
      }
   }
}
