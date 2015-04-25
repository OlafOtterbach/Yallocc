namespace ParserLib.FluentInterface
{
   class BuilderInterface<T>
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

      public BranchInterface<T> CreateBranch()
      {
         return new BranchInterface<T>();
      }
   }
}
