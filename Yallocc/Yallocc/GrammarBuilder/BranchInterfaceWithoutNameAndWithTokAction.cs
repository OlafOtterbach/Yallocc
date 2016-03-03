/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>
using Yallocc.Tokenizer;
using System;

namespace Yallocc
{
   public class BranchInterfaceWithoutNameAndWithTokAction<TCtx, T> : BranchInterface<TCtx,T> where T : struct
   {
      public BranchInterfaceWithoutNameAndWithTokAction(GrammarBuilder<TCtx, T> grammarBuilder)
         : base(grammarBuilder)
      {}

      public BranchInterface<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterface<TCtx, T>(GrammarBuilder);
      }

      public BranchInterface<TCtx, T> Action(Action<TCtx, Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
