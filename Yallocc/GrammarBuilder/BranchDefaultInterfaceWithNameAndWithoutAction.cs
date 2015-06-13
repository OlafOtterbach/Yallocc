namespace Yallocc
{
   public class BranchDefaultInterfaceWithNameAndWithoutAction<T> : BranchBuilder<T> where T : struct
   {
      public BranchDefaultInterfaceWithNameAndWithoutAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      { }

      public BranchDefaultInterfaceWithoutNameAndWithoutAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchDefaultInterfaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
