/// <author>Olaf Otterbach</author>
/// <date>25.12.2015</date>

namespace Yallocc.SyntaxTree
{
   public class SyntaxTreeGenerator<T> where T : struct
   {
      private ParserAndTokenizer<SyntaxTreeBuilder,T> _parser;

      public SyntaxTreeGenerator(ParserAndTokenizer<SyntaxTreeBuilder, T> parser)
      {
          _parser = parser;
      }

      public SyntaxTreeResult<T> Parse(string text)
      {
         if (_parser == null)
         {
            throw new SyntaxTreeGeneratorNotReadyException("Not initalized generator.");
         }

         var syntaxTreeBuilder = new SyntaxTreeBuilder();
         var parseResult = _parser.Parse(text, syntaxTreeBuilder);
         var result = new SyntaxTreeResult<T>(syntaxTreeBuilder.Root, parseResult);
         return result;
      }
   }
}
