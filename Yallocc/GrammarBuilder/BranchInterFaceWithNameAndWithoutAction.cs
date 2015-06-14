namespace Yallocc
{
   public class BranchInterFaceWithNameAndWithoutAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterFaceWithNameAndWithoutAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterface<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterface<T>(GrammarBuilder);
      }
   }
}
