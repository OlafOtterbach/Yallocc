namespace SyntaxTree
{
   public class GeneratorInterfaceWithRegisterWithoutCreate<T> where T : struct
   {
      private SyntaxTreeGenerator<T> _generator;

      public GeneratorInterfaceWithRegisterWithoutCreate(SyntaxTreeGenerator<T> generator)
      {
         _generator = generator;
      }

      public GeneratorInterfaceWithRegisterWithCreate<T> Register(ITokenAndGrammarDefinition<T> grammarDefinition)
      {
         _generator.Register(grammarDefinition);
         return new GeneratorInterfaceWithRegisterWithCreate<T>(_generator);
      }
   }
}
