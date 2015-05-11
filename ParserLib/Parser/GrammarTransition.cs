namespace ParserLib
{
   public class GrammarTransition : ActionTransition
   {
      public GrammarTransition(Transition start) : base()
      {
         Start = start;
      }

      public Transition Start { get; private set; }
   }
}
