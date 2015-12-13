using Yallocc.Tokenizer;

namespace Yallocc
{
   public class ParserAndTokenizer<TCtx,T> where T : struct
   {
      private Tokenizer<T> _tokenizer;
      private SyntaxDiagramParser<TCtx,T> _parser;

      public ParserAndTokenizer(SyntaxDiagramParser<TCtx,T> parser, Tokenizer<T> tokenizer)
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
