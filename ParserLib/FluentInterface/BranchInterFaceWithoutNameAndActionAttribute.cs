namespace ParserLib
{
   public class BranchInterFaceWithoutNameAndActionAttribute<T> : BranchInterface<T>
   {
      public BranchInterFaceWithoutNameAndActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {
      }
   }
}
