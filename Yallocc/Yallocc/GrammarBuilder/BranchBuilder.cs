namespace Yallocc
{
   public class BranchBuilder<TCtx, T> where T : struct
   {
      private GrammarBuilder<TCtx, T> _grammarBuilder;

      public BranchBuilder(GrammarBuilder<TCtx, T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public GrammarBuilder<TCtx, T> GrammarBuilder
      {
         get
         {
            return _grammarBuilder;
         }
      }
   }
}
