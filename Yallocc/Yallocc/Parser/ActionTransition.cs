using System;

namespace Yallocc
{
   public class ActionTransition<TCtx> : Transition
   {
      public ActionTransition()
      {
         Action = (TCtx ctx) => {};
      }

      public ActionTransition(Action<TCtx> action)
      {
         Action = action;
      }

      public Action<TCtx> Action { get; set; }
   }
}
