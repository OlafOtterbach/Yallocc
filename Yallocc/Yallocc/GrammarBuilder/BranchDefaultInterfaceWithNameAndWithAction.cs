/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

using System;

namespace Yallocc
{
   public class BranchDefaultInterfaceWithNameAndWithAction<TCtx, T> : BranchBuilder<TCtx, T> where T : struct
   {
      public BranchDefaultInterfaceWithNameAndWithAction(GrammarBuilder<TCtx, T> grammarBuilder)
         : base(grammarBuilder)
      {
      }

      public BranchDefaultInterfaceWithoutNameAndWithAction<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchDefaultInterfaceWithoutNameAndWithAction<TCtx, T>(GrammarBuilder);
      }

      public BranchDefaultInterfaceWithNameAndWithoutAction<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchDefaultInterfaceWithNameAndWithoutAction<TCtx, T>(GrammarBuilder);
      }
   }
}
