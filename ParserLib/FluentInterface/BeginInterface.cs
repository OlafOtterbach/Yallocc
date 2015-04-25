namespace ParserLib
{
   public class BeginInterface<T>
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BeginInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public ProducerInterface<T> BeginGrammar()
      {
         return new ProducerInterface<T>(_grammarBuilder);
      }
   }
}
