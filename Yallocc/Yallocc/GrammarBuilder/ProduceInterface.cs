using System;
using System.Linq;

namespace Yallocc
{
   public class ProduceInterface<TCtx,T> where T : struct
   {
      public ProduceInterface(GrammarBuilder<TCtx,T> grammarBuilder)
      {
         GrammarBuilder = grammarBuilder;
      }

      protected GrammarBuilder<TCtx,T> GrammarBuilder { get; }

      public ExitInterfaceWithNameAndWithAction<TCtx, T> Exit
      {
         get
         {
            GrammarBuilder.AddLambda();
            return new ExitInterfaceWithNameAndWithAction<TCtx,T>(GrammarBuilder);
         }
      }

      public ProduceInterfaceWithNameAndWithTokAction<TCtx, T> Token(T tokenType)
      {
         GrammarBuilder.AddToken(tokenType);
         return new ProduceInterfaceWithNameAndWithTokAction<TCtx, T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndWithAction<TCtx, T> Label(string label)
      {
         GrammarBuilder.AddLabel(label);
         return new ProduceInterfaceWithNameAndWithAction<TCtx, T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndWithAction<TCtx, T> Lambda
      {
         get
         {
            GrammarBuilder.AddLambda();
            return new ProduceInterfaceWithNameAndWithAction<TCtx, T>(GrammarBuilder);
         }
      }

      public GosubInterfaceWithoutNameAndWithAction<TCtx, T> Gosub(string nameOfSubGrammar)
      {
         GrammarBuilder.AddSubGrammar(nameOfSubGrammar);
         return new GosubInterfaceWithoutNameAndWithAction<TCtx, T>(GrammarBuilder);
      }

      public ProduceInterface<TCtx, T> Goto(string label)
      {
         GrammarBuilder.GotoLabel(label);
         return new ProduceInterface<TCtx, T>(GrammarBuilder);
      }

      public ProduceInterface<TCtx, T> Switch(params BranchBuilder<TCtx, T>[] branches)
      {
         GrammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return new ProduceInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
