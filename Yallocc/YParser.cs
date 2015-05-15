using LexSharp;

namespace Yallocc
{
   public class YParser<T>
   {
      private YGrammar _grammar;
      private LexSharp<T> _lex;
      private Parser<T> _parser;

      public YParser(YGrammar grammar, LexSharp<T> lex)
      {
         _lex = lex;
         _grammar = grammar;
         _parser = new Parser<T>();
      }

      public ParserResult Parse(string text)
      {
         var sequence = _lex.Scan(text);
         var result = _parser.ParseTokens(_grammar.StartOfGrammar, sequence);
         return result;
      }
   }
}
