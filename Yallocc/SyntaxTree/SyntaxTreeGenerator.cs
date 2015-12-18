using System.Collections.Generic;
using Yallocc;

namespace SyntaxTree
{
   public class SyntaxTreeGenerator<T> where T : struct
   {
      private SyntaxTreeBuilder _syntaxTreeBuilder;
      private ParserAndTokenizer<SyntaxTreeBuilder,T> _parser;

      private SyntaxTreeGenerator(ParserAndTokenizer<SyntaxTreeBuilder, T> parser, SyntaxTreeBuilder syntaxTreeBuilder)
      {
         _syntaxTreeBuilder = syntaxTreeBuilder;
         _parser = parser;
      }

      public SyntaxTreeBuilderResult Parse(string text)
      {
         if (_parser == null)
         {
            throw new SyntaxTreeGeneratorNotReadyException("Not initalized generator.");
         }

         var syntaxTreeBuilder = new SyntaxTreeBuilder();
         var parseResult = _parser.Parse(text);
         var result = new SyntaxTreeBuilderResult(syntaxTreeBuilder.Root, parseResult);
         return result;
      }
   }
}
