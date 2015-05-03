namespace ParserLib
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
