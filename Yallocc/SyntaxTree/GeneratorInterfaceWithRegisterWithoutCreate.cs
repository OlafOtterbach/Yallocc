namespace Yallocc.SyntaxTree
{
   public class GeneratorInterfaceWithRegisterWithoutCreate<T> where T : struct
   {
      private SyntaxTreeGeneratorCreator<T> _creator;

      public GeneratorInterfaceWithRegisterWithoutCreate(SyntaxTreeGeneratorCreator<T> creator)
      {
         _creator = creator;
      }

      public GeneratorInterfaceWithRegisterWithCreate<T> Register(ITokenAndGrammarDefinition<T> grammarDefinition)
      {
         _creator.Register(grammarDefinition);
         return new GeneratorInterfaceWithRegisterWithCreate<T>(_creator);
      }
   }
}
