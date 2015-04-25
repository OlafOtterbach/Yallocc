using System;

namespace ParserLib
{
   public class ProxyTransition : Transition
   {
      public ProxyTransition(string name)
         : base()
      {
         Name = name;
      }

      public ProxyTransition(string name, Action action)
         : base(action)
      {
         Name = name;
      }
   }
}
