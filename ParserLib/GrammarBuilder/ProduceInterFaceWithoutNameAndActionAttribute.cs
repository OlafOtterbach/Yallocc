namespace ParserLib
{
   public class ProduceInterFaceWithoutNameAndActionAttribute<T> : ProduceInterface<T>
   {
      public ProduceInterFaceWithoutNameAndActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}
   }
}
