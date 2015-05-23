namespace Yallocc
{
   public class GrammarTransition : ActionTransition
   {
      public GrammarTransition(Transition start) : base()
      {
         Start = start;
      }

      public Transition Start { get; public set; }
   }
}
