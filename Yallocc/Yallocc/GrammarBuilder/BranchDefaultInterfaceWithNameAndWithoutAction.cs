namespace Yallocc
{
   public class BranchDefaultInterfaceWithNameAndWithoutAction<TCtx, T> : BranchBuilder<TCtx, T> where T : struct
   {
      public BranchDefaultInterfaceWithNameAndWithoutAction(GrammarBuilder<TCtx, T> grammarBuilder)
         : base(grammarBuilder)
      { }

      public BranchBuilder<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchBuilder<TCtx, T>(GrammarBuilder);
      }
   }
}
