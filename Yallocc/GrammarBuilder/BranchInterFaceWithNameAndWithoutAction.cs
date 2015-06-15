namespace Yallocc
{
   public class BranchInterfaceWithNameAndWithoutAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterfaceWithNameAndWithoutAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterface<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterface<T>(GrammarBuilder);
      }
   }
}
