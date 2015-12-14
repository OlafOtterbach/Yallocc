using System.Linq;

namespace Yallocc
{
   public class BranchInterface<TCtx, T> : BranchBuilder<TCtx, T> where T : struct
   {
      public BranchInterface(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterfaceWithNameAndWithTokAction<TCtx, T> Token(T tokenType)
      {
         GrammarBuilder.AddToken(tokenType);
         return new BranchInterfaceWithNameAndWithTokAction<TCtx, T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndWithAction<TCtx, T> Label(string label)
      {
         GrammarBuilder.AddLabel(label);
         return new BranchInterfaceWithNameAndWithAction<TCtx, T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndWithAction<TCtx, T> Lambda
      {
         get
         {
            GrammarBuilder.AddLambda();
            return new BranchInterfaceWithNameAndWithAction<TCtx, T>(GrammarBuilder);
         }
      }

      public BranchDefaultInterfaceWithNameAndWithAction<TCtx, T> Default
      {
         get
         {
            GrammarBuilder.AddLambda();
            return new BranchDefaultInterfaceWithNameAndWithAction<TCtx, T>(GrammarBuilder);
         }
      }

      public BranchGosubInterfaceWithoutNameAndWithAction<TCtx, T> Gosub(string nameOfSubGrammar)
      {
         GrammarBuilder.AddSubGrammar(nameOfSubGrammar);
         return new BranchGosubInterfaceWithoutNameAndWithAction<TCtx, T>(GrammarBuilder);
      }

      public BranchInterface<TCtx, T> Goto(string label)
      {
         GrammarBuilder.GotoLabel(label);
         return new BranchInterface<TCtx, T>(GrammarBuilder);
      }

      public BranchInterface<TCtx, T> Switch(params BranchBuilder<TCtx, T>[] branches)
      {
         GrammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return new BranchInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
