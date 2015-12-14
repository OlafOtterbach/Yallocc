namespace Yallocc
{
   public class BranchInterfaceWithNameAndWithoutAction<TCtx, T> : BranchInterface<TCtx, T> where T : struct
   {
      public BranchInterfaceWithNameAndWithoutAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterface<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
