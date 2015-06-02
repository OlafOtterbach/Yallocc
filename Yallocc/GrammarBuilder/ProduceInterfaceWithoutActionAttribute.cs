namespace Yallocc
{
   public class ProduceInterFaceWithoutActionAttribute<T> : ProduceInterface<T> where T : struct
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
