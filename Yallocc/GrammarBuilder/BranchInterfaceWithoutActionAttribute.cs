namespace Yallocc
{
   public class BranchInterFaceWithoutActionAttribute<T> : BranchInterface<T>
   {
      public BranchInterFaceWithoutActionAttribute(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterFaceWithoutNameAndActionAttribute<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
