namespace Yallocc
{
   public class ProduceInterFaceWithNameAndWithoutAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterFaceWithNameAndWithoutAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProduceInterFaceWithoutNameAndWithoutAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterFaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
