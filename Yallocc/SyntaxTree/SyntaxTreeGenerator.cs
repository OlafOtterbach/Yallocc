using Yallocc;

namespace SyntaxTree
{
   public class SyntaxTreeGenerator<T> where T : struct
   {
      private ParserAndTokenizer<SyntaxTreeBuilder,T> _parser;

      public SyntaxTreeGenerator(ParserAndTokenizer<SyntaxTreeBuilder, T> parser)
      {
          _parser = parser;
      }

      public SyntaxTreeBuilderResult Parse(string text)
      {
         if (_parser == null)
         {
            throw new SyntaxTreeGeneratorNotReadyException("Not initalized generator.");
         }

         var syntaxTreeBuilder = new SyntaxTreeBuilder();
         var parseResult = _parser.Parse(text, syntaxTreeBuilder);
         var result = new SyntaxTreeBuilderResult(syntaxTreeBuilder.Root, parseResult);
         return result;
      }
   }
}
