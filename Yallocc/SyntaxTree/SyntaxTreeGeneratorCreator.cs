using System.Collections.Generic;
using System.Linq;
using Yallocc;
using Yallocc.Tokenizer;

namespace Yallocc.SyntaxTree
{
   public class SyntaxTreeGeneratorCreator<T> where T : struct
   {
      private readonly TokenizerCreator<T> _tokenizerCreator;

      private readonly List<ITokenAndGrammarDefinition<T>> _grammarDefinitions;

      private SyntaxTreeBuilder _syntaxTreeBuilder;


      private SyntaxTreeGeneratorCreator(TokenizerCreator<T> tokenizerCreator)
      {
         _tokenizerCreator = tokenizerCreator;
         _grammarDefinitions = new List<ITokenAndGrammarDefinition<T>>();
      }


      public static GeneratorInterfaceWithRegisterWithoutCreate<T> RegisterDefinitions(TokenizerCreator<T> tokenizerCreator)
      {
         return new GeneratorInterfaceWithRegisterWithoutCreate<T>(new SyntaxTreeGeneratorCreator<T>(tokenizerCreator));
      }


      internal void Register(ITokenAndGrammarDefinition<T> grammarDefinition)
      {
         _grammarDefinitions.Add(grammarDefinition);
      }


      public SyntaxTreeGenerator<T> Create()
      {
         if (!_grammarDefinitions.Any())
         {
            throw new SyntaxTreeGeneratorNotReadyException("No grammar definition defined.");
         }

         _syntaxTreeBuilder = new SyntaxTreeBuilder();
         var yallocc = new Yallocc<SyntaxTreeBuilder, T>(_tokenizerCreator);
         _grammarDefinitions.ForEach(g => g.Define(yallocc));
         var parser = yallocc.CreateParser();
         var generator = new SyntaxTreeGenerator<T>(parser);
         return generator;
      }
   }
}
