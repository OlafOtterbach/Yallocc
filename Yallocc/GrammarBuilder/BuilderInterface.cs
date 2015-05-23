namespace Yallocc
{
   public class BuilderInterface<T>
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BuilderInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public BeginInterface<T> Grammar(string name)
      {
         _grammarBuilder.CreateGrammar(name);
         return new BeginInterface<T>(_grammarBuilder);
      }

      public BranchInterFaceWithoutNameAndActionAttribute<T> Branch
      {
         get
         {
            return new BranchInterFaceWithoutNameAndActionAttribute<T>(_grammarBuilder.CreateBranchBuilder());
         }
      }
   }
}
