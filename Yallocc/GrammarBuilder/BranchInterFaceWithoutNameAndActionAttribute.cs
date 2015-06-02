namespace Yallocc
{
   public class BranchInterFaceWithoutNameAndActionAttribute<T> : BranchInterface<T> where T : struct
   {
      public BranchInterFaceWithoutNameAndActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }
   }
}
