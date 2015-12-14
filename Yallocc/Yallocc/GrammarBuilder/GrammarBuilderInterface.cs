namespace Yallocc
{
   public class GrammarBuilderInterface<TCtx,T> where T : struct
   {
      private GrammarBuilder<TCtx,T> _grammarBuilder;

      public GrammarBuilderInterface(GrammarBuilder<TCtx,T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public EnterInterface<TCtx, T> MasterGrammar(string name)
      {
         _grammarBuilder.CreateMasterGrammar(name);
         return new EnterInterface<TCtx, T>(_grammarBuilder);
      }

      public EnterInterface<TCtx, T> Grammar(string name)
      {
         _grammarBuilder.CreateGrammar(name);
         return new EnterInterface<TCtx, T>(_grammarBuilder);
      }

      public BranchInterface<TCtx, T> Branch
      {
         get
         {
            return new BranchInterface<TCtx, T>(_grammarBuilder.CreateBranchBuilder());
         }
      }
   }
}
