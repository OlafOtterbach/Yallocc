﻿using System;

namespace ParserLib
{
   public class LabelTransition : ActionTransition
   {
      public LabelTransition(string name) : base()
      {
         Name = name;
      }
   }
}
