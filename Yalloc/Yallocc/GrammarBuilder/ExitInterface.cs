namespace Yallocc
{
   public class ExitInterface<T> where T : struct
   {
      private GrammarBuilder<T> _grammarBuilder;

      public ExitInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public GrammarBuilder<T> GrammarBuilder
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
