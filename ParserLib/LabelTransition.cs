using System;

namespace ParserLib
{
   public class LabelTransition : Transition
   {
      public LabelTransition(string name) : base()
      {
         Name = name;
      }

      public LabelTransition( string name, Action action ) : base(action)
      {
         Name = name;
      }
   }
}
