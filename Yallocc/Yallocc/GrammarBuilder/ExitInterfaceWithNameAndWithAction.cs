/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>
using System;

namespace Yallocc
{
   public class ExitInterfaceWithNameAndWithAction<TCtx, T> : ExitInterface<TCtx, T> where T : struct
   {
      public ExitInterfaceWithNameAndWithAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public ExitInterfaceWithoutNameAndWithAction<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ExitInterfaceWithoutNameAndWithAction<TCtx, T>(GrammarBuilder);
      }

      public ExitInterfaceWithNameAndWithoutAction<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new ExitInterfaceWithNameAndWithoutAction<TCtx, T>(GrammarBuilder);
      }
   }
}
