using System.Collections.Generic;

namespace ParserLib
{
   public class Transition
   {
      private List<Transition> _successors;

      public Transition()
      {
         _successors = new List<Transition>();
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
   }
}
