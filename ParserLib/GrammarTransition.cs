namespace ParserLib
{
   public class GrammarTransition : Transition
   {
      public GrammarTransition(Transition start)
      {
         Start = start;
      }

      public Transition Start { get; private set; }
   }
}
