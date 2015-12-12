using Yallocc.Tokenizer;

namespace Yallocc
{
   public class ParserAndTokenizer<T> where T : struct
   {
      private Tokenizer<T> _tokenizer;
      private SyntaxDiagramParser<T> _parser;

      public ParserAndTokenizer(SyntaxDiagramParser<T> parser, Tokenizer<T> tokenizer)
      {
         _tokenizer = tokenizer;
         _parser = parser;
      }

      public ParserResult Parse(string text)
      {
         var sequence = _tokenizer.Scan(text);
         var result = _parser.ParseTokens(sequence);
         return result;
      }
   }
}
