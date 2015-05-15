namespace ParserLib
{
   public class ProxyTransition : Transition
   {
      public ProxyTransition(string targetName)
         : base()
      {
         TargetName = targetName;
      }

      public string TargetName { get; private set; }
   }
}
