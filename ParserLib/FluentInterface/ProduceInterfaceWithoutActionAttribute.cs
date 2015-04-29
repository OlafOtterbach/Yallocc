namespace ParserLib
{
   public class ProducerInterFaceWithoutActionAttribute<T> : ProducerInterface<T>
   {
      public ProducerInterFaceWithoutActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProducerInterFaceWithoutNameAndActionAttribute<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProducerInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
