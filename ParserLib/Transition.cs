﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace ParserLib
{
   public class Transition
   {
      private List<Transition> _successors;

      public Transition()
      {
         _successors = new List<Transition>();
         Action = null;
         Name = string.Empty;
      }

      public Transition(Action action)
      {
         _successors = new List<Transition>();
         Action = action;
         Name = string.Empty;
      }

      public IEnumerable<Transition> Successors
      {
         get
         {
            return _successors;
         }
      }

      public string Name { get; set; }

      public Action Action { get; set; }

      public void AddSuccessor(Transition transition)
      {
         _successors.Add(transition);
      }

      public void AddSuccessors(IEnumerable<Transition> successorsToAdd)
      {
         _successors.AddRange(successorsToAdd.Where(x => !_successors.Contains(x)));
      }

      public void RemoveSuccessors(IEnumerable<Transition> successorsToRemove)
      {
         successorsToRemove.Where(x => _successors.Contains(x)).ToList().ForEach(s => _successors.Remove(s));
      }

      public void Execute()
      {
         if(Action != null)
         {
            Action();
         }
      }
   }
}
