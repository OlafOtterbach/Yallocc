/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

namespace Yallocc
{
   public class LabelTransition<TCtx> : ActionTransition<TCtx>
   {
      public LabelTransition(string name) : base()
      {
         Name = name;
      }
   }
}
