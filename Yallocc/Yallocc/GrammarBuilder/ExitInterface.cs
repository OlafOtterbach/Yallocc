namespace Yallocc
{
   public class ExitInterface<TCtx, T> where T : struct
   {
      private GrammarBuilder<TCtx, T> _grammarBuilder;

      public ExitInterface(GrammarBuilder<TCtx, T> grammarBuilder)
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

      public void EndGrammar()
      {
         _grammarBuilder.EndGrammar();
      }
   }
}
