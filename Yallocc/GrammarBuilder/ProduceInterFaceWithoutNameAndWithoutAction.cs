namespace Yallocc
{
   public class ProduceInterFaceWithoutNameAndWithoutAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterFaceWithoutNameAndWithoutAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}
   }
}
