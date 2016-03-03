/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>
using System;

namespace Yallocc
{
   public class BranchInterFaceWithoutNameAndWithAction<TCtx, T> : BranchInterface<TCtx, T> where T : struct
   {
      public BranchInterFaceWithoutNameAndWithAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterface<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
