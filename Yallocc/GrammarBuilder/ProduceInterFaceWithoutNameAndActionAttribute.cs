namespace Yallocc
{
   public class ProduceInterFaceWithoutNameAndActionAttribute<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterFaceWithoutNameAndActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}
   }
}
