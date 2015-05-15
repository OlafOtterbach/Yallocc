namespace Yallocc
{
   public class BuilderInterface<T>
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BuilderInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public BeginInterface<T> CreateGrammar()
      {
         _grammarBuilder.Reset();
         return new BeginInterface<T>(_grammarBuilder);
      }

      public BranchInterFaceWithoutNameAndActionAttribute<T> Branch
      {
         get
         {
            return new BranchInterFaceWithoutNameAndActionAttribute<T>(new GrammarBuilder<T>());
         }
      }
   }
}
