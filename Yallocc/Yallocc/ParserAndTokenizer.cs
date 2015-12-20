using Yallocc.Tokenizer;

namespace Yallocc
{
   public class ParserAndTokenizer<TCtx,T> where T : struct
   {
      private Tokenizer<T> _tokenizer;
      private SyntaxDiagramParser<TCtx,T> _parser;

      public Tokenizer<T> Tokenizer
      {
         get
         {
            return _tokenizer;
         }
      }

      public SyntaxDiagramParser<TCtx,T> Parser
      {
         get
         {
            return _parser;
         }
      }

      public ParserAndTokenizer(SyntaxDiagramParser<TCtx,T> parser, Tokenizer<T> tokenizer)
      {
         _tokenizer = tokenizer;
         _parser = parser;
      }

      public ParserResult Parse(string text, TCtx context)
      {
         var sequence = _tokenizer.Scan(text);
         var result = _parser.ParseTokens(sequence, context);
         return result;
      }
   }
}
