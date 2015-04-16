using System;
using System.Collections.Generic;

namespace ParserLib
{
   public class Transition
   {
      private List<Transition> _successors;

      Action _action;

      public Transition()
      {
         _successors = new List<Transition>();
         _action = null;
      }

      public Transition(Action action)
      {
         _successors = new List<Transition>();
         _action = action;
      }

      public IEnumerable<Transition> Successors
      {
         get
         {
            return _successors;
         }
      }

      public string Name { get; set; }

      public void AddSuccessor(Transition transition)
      {
         _successors.Add(transition);
      }

      public void Execute()
      {
         if(_action != null)
         {
            _action();
         }
      }
   }
}
