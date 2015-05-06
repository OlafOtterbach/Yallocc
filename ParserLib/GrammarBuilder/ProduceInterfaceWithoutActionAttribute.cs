namespace ParserLib
{
   public class ProduceInterFaceWithoutActionAttribute<T> : ProduceInterface<T>
   {
      public ProduceInterFaceWithoutActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProduceInterFaceWithoutNameAndActionAttribute<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
