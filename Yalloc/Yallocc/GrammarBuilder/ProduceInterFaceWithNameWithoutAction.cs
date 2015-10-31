namespace Yallocc
{
   public class ProduceInterfaceWithNameAndWithoutAction<T> : ProduceInterface<T> where T : struct
   {
      public ProduceInterfaceWithNameAndWithoutAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProduceInterface<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterface<T>(GrammarBuilder);
      }
   }
}
