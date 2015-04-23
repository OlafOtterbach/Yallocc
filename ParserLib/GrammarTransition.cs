using System;

namespace ParserLib
{
   public class GrammarTransition : Transition
   {
      public GrammarTransition(Transition start) : base()
      {
         Start = start;
      }

      public GrammarTransition(string name, Transition start) : base()
      {
         Name = name;
         Start = start;
      }

      public GrammarTransition(Transition start, Action action) : base(action)
      {
         Start = start;
      }

      public GrammarTransition(string name, Transition start, Action action) : base(action)
      {
         Name = name;
         Start = start;
      }

      public Transition Start { get; private set; }
   }
}
