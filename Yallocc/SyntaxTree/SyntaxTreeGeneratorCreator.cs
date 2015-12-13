using System.Collections.Generic;
using System.Linq;
using Yallocc;
using Yallocc.Tokenizer;

namespace SyntaxTree
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

      public static GeneratorInterfaceWithRegisterWithoutCreate<T> Make(TokenizerCreator<T> tokenizerCreator)
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

         var yallocc = new Yallocc<T>(_tokenizerCreator);
         _syntaxTreeBuilder = new SyntaxTreeBuilder();
         _grammarDefinitions.ForEach(g => g.Define(yallocc, _syntaxTreeBuilder));
         var parser = yallocc.CreateParser();
         return null;
      }
   }
}
