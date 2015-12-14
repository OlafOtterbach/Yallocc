using System;

namespace Yallocc
{
   public class ExitInterfaceWithoutNameAndWithAction<TCtx, T> : ExitInterface<TCtx, T> where T : struct
   {
      public ExitInterfaceWithoutNameAndWithAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public ExitInterface<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new ExitInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
