/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

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
