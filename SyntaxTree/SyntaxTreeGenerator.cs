using System.Linq;
using System.Collections.Generic;
using Yallocc;

namespace SyntaxTree
{
   public class SyntaxTreeGenerator<T> where T : struct
   {
      private List<ITokenAndGrammarDefinition<T>> _grammarDefinitions; 
      private SyntaxTreeBuilder _syntaxTreeBuilder;
      private YParser<T> _parser;

      private SyntaxTreeGenerator()
      {
         _grammarDefinitions = new List<ITokenAndGrammarDefinition<T>>();
         _parser = null;
      }

      public static GeneratorInterfaceWithRegisterWithoutCreate<T> Make()
      {
         var generator = new SyntaxTreeGenerator<T>();
         return new GeneratorInterfaceWithRegisterWithoutCreate<T>(generator);
      }

      internal void Register(ITokenAndGrammarDefinition<T> grammarDefinition)
      {
         _grammarDefinitions.Add(grammarDefinition);
      }

      internal void Init()
      {
         if (!_grammarDefinitions.Any())
         {
            throw new SyntaxTreeGeneratorNotReadyException("No grammar definition defined.");
         }

         var yacc = new Yallocc<T>();
         _syntaxTreeBuilder = new SyntaxTreeBuilder();
         _grammarDefinitions.ForEach(g => g.Define(yacc, _syntaxTreeBuilder));
         _parser = yacc.CreateParser();
      }

      public SyntaxTreeBuilderResult Parse(string text)
      {
         if (_parser == null)
         {
            throw new SyntaxTreeGeneratorNotReadyException("Not initalized generator.");
         }

         _syntaxTreeBuilder.Reset();
         var parseResult = _parser.Parse(text);
         var result = new SyntaxTreeBuilderResult(_syntaxTreeBuilder.Root, parseResult);
         return result;
      }
   }
}
