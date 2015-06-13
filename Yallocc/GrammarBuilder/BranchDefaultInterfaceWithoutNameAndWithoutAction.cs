namespace Yallocc
{
   public class BranchDefaultInterfaceWithoutNameAndWithoutAction<T> : BranchBuilder<T> where T : struct
   {
      public BranchDefaultInterfaceWithoutNameAndWithoutAction(GrammarBuilder<T> grammarBuilder)
         : base(grammarBuilder)
      {
      }
   }
}
