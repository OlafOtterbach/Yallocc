/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

namespace Yallocc
{
   public class ProxyTransition : Transition
   {
      public ProxyTransition(string targetName)
         : base()
      {
         TargetName = targetName;
      }

      public string TargetName { get; }
   }
}
