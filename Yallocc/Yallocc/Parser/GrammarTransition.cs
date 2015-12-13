namespace Yallocc
{
   public class GrammarTransition<TCtx> : ActionTransition<TCtx>
   {
      public GrammarTransition(Transition start) : base()
      {
         Start = start;
      }

      public Transition Start { get; set; }
   }
}
