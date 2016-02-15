using System.Linq;

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

      public GeneratorInterfaceWithRegisterWithCreate<T> Register(params ITokenAndGrammarDefinition<T>[] grammarDefinitions)
      {
         grammarDefinitions.ToList().ForEach(def => _creator.Register(def));
         return new GeneratorInterfaceWithRegisterWithCreate<T>(_creator);
      }
   }
}
