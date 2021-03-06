﻿/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

using System;

namespace Yallocc
{
   public class BranchDefaultInterfaceWithoutNameAndWithAction<TCtx, T> : BranchBuilder<TCtx, T> where T : struct
   {
      public BranchDefaultInterfaceWithoutNameAndWithAction(GrammarBuilder<TCtx, T> grammarBuilder)
         : base(grammarBuilder)
      { }

      public BranchBuilder<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchBuilder<TCtx, T>(GrammarBuilder);
      }
   }
}
