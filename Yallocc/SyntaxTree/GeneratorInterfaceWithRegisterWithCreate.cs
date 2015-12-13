namespace SyntaxTree
{
   public class GeneratorInterfaceWithRegisterWithCreate<T> where T : struct
   {
      private SyntaxTreeGeneratorCreator<T> _creator;

      internal GeneratorInterfaceWithRegisterWithCreate(SyntaxTreeGeneratorCreator<T> creator)
      {
         _creator = creator;
      }

      public GeneratorInterfaceWithRegisterWithCreate<T> Register(ITokenAndGrammarDefinition<T> grammarDefinition)
      {
         _creator.Register(grammarDefinition);
         return new GeneratorInterfaceWithRegisterWithCreate<T>(_creator);
      }

      public SyntaxTreeGenerator<T> Create()
      {
         return _creator.Create();
      }
   }
}
