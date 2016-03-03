/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

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
