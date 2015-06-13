namespace Yallocc
{
   public class BuilderInterface<T> where T : struct
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BuilderInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public EnterInterface<T> MasterGrammar(string name)
      {
         _grammarBuilder.CreateMasterGrammar(name);
         return new EnterInterface<T>(_grammarBuilder);
      }

      public EnterInterface<T> Grammar(string name)
      {
         _grammarBuilder.CreateGrammar(name);
         return new EnterInterface<T>(_grammarBuilder);
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
