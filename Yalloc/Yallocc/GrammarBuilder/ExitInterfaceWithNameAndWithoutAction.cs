namespace Yallocc
{
   public class ExitInterfaceWithNameAndWithoutAction<T> : ExitInterface<T> where T : struct
   {
      public ExitInterfaceWithNameAndWithoutAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ExitInterface<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ExitInterface<T>(GrammarBuilder);
      }
   }
}
