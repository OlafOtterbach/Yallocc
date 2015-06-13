namespace Yallocc
{
   public class BranchInterFaceWithNameAndWithoutAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterFaceWithNameAndWithoutAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterFaceWithoutNameAndWithoutAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterFaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
