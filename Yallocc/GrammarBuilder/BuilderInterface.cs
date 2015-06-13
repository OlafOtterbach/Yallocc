namespace Yallocc
{
   public class BuilderInterface<T> where T : struct
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BuilderInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public BeginInterface<T> MasterGrammar(string name)
      {
         _grammarBuilder.CreateMasterGrammar(name);
         return new BeginInterface<T>(_grammarBuilder);
      }

      public BeginInterface<T> Grammar(string name)
      {
         _grammarBuilder.CreateGrammar(name);
         return new BeginInterface<T>(_grammarBuilder);
      }

      public BranchInterFaceWithoutNameAndWithoutAction<T> Branch
      {
         get
         {
            return new BranchInterFaceWithoutNameAndWithoutAction<T>(_grammarBuilder.CreateBranchBuilder());
         }
      }
   }
}
