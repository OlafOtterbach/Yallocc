namespace SyntaxTree
{
   public class GeneratorInterfaceWithRegisterWithCreate<T> where T : struct
   {
      private SyntaxTreeGenerator<T> _generator;

      internal GeneratorInterfaceWithRegisterWithCreate(SyntaxTreeGenerator<T> generator)
      {
         _generator = generator;
      }

      public GeneratorInterfaceWithRegisterWithCreate<T> Register(ITokenAndGrammarDefinition<T> grammarDefinition)
      {
         _generator.Register(grammarDefinition);
         return new GeneratorInterfaceWithRegisterWithCreate<T>(_generator);
      }

      public SyntaxTreeGenerator<T> Create()
      {
         _generator.Init();
         return _generator;
      }
   }
}
