namespace ParserLib
{
   public class BranchBuilder<T>
   {
      private GrammarBuilder<T> _grammarBuilder;

      protected BranchBuilder(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      internal GrammarBuilder<T> GrammarBuilder
      {
         get
         {
            return _grammarBuilder;
         }
      }
   }
}
