using LexSharp;

namespace Yallocc
{
   public class YParser<T> where T : struct
   {
      private Transition _startOfGrammar;
      private LexSharp<T> _lex;
      private Parser<T> _parser;

      public YParser(Transition startOfGrammar, LexSharp<T> lex)
      {
         _lex = lex;
         _startOfGrammar = startOfGrammar;
         _parser = new Parser<T>();
      }

      public ParserResult Parse(string text)
      {
         var sequence = _lex.Scan(text);
         var result = _parser.ParseTokens(_startOfGrammar, sequence);
         return result;
      }
   }
}
