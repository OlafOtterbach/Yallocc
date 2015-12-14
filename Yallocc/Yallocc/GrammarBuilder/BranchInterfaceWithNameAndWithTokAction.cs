using System;
using Yallocc.Tokenizer;

namespace Yallocc
{
   public class BranchInterfaceWithNameAndWithTokAction<TCtx, T> : BranchInterface<TCtx, T> where T : struct
   {
      public BranchInterfaceWithNameAndWithTokAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {
      }

      public BranchInterfaceWithoutNameAndWithTokAction<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterfaceWithoutNameAndWithTokAction<TCtx, T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndWithoutAction<TCtx, T> Action(Action<TCtx> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterfaceWithNameAndWithoutAction<TCtx, T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndWithoutAction<TCtx, T> Action(Action<TCtx, Token<T>> action)
      {
         GrammarBuilder.AddAction(action);
         return new BranchInterfaceWithNameAndWithoutAction<TCtx, T>(GrammarBuilder);
      }
   }
}
