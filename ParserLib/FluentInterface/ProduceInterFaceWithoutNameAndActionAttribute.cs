namespace ParserLib
{
   public class ProducerInterFaceWithoutNameAndActionAttribute<T> : ProducerInterface<T>
   {
      public ProducerInterFaceWithoutNameAndActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}
   }
}
