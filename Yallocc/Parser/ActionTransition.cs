using System;

namespace Yallocc
{
   public class ActionTransition : Transition
   {
      public ActionTransition()
      {
         Action = () => {};
      }

      public ActionTransition(Action action)
      {
         Action = action;
      }

      public Action Action { get; set; }
   }
}
