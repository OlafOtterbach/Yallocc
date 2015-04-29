namespace ParserLib
{
   public class BeginInterface<T>
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BeginInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public ProducerInterFaceWithoutNameAndActionAttribute<T> Begin()
      {
         return new ProducerInterFaceWithoutNameAndActionAttribute<T>(_grammarBuilder);
      }
   }
}
