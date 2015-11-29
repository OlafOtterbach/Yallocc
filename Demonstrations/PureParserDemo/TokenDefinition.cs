using LexSharp;

namespace PureParserDemo
{
   public static class TokenDefinition
   {
      public static void DefineTokens(this ITokenizer<TokenType> lex)
      {
         lex.Register(@"\+", TokenType.plus);
         lex.Register(@"\-", TokenType.minus);
         lex.Register(@"\*", TokenType.mult);
         lex.Register(@"\/", TokenType.div);
         lex.Register(@"\(", TokenType.open);
         lex.Register(@"\)", TokenType.close);
         lex.Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         lex.Register(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         lex.RegisterIgnorePattern(@"( |\t)+", TokenType.white_space);
      }
   }
}
