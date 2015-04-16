using System;

namespace ParserLib
{
   public class GrammarTransition : Transition
   {
      public GrammarTransition(Transition start) : base()
      {
         Start = start;
      }

      public GrammarTransition(Transition start, Action action) : base(action)
      {
         Start = start;
      }

      public Transition Start { get; private set; }
   }
}
