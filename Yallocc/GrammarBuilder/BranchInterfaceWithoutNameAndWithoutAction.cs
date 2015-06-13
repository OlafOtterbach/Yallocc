namespace Yallocc
{
   public class BranchInterFaceWithoutNameAndWithoutAction<T> : BranchInterface<T> where T : struct
   {
      public BranchInterFaceWithoutNameAndWithoutAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }
   }
}
