using LexSharp;

namespace Yallocc
{
   public class YParser<T> where T : struct
   {
      private Transition _startOfGrammar;
      private LeTok<T> _tokenizer;
      private Parser<T> _parser;

      public YParser(Transition startOfGrammar, LeTok<T> lex)
      {
         _tokenizer = lex;
         _startOfGrammar = startOfGrammar;
         _parser = new Parser<T>();
      }

      public ParserResult Parse(string text)
      {
         var sequence = _tokenizer.Scan(text);
         var result = _parser.ParseTokens(_startOfGrammar, sequence);
         return result;
      }
   }
}
