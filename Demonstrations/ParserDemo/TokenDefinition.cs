using Yallocc;

namespace ParserDemo
{
   public static class TokenDefinition
   {
      public static void DefineParserTokens(this Yallocc<DummyContext, TokenType> yacc)
      {
         yacc.DefineTokens()
             .AddTokenPattern(@"\+", TokenType.plus)
             .AddTokenPattern(@"\-", TokenType.minus)
             .AddTokenPattern(@"\*", TokenType.mult)
             .AddTokenPattern(@"\/", TokenType.div)
             .AddTokenPattern(@"\(", TokenType.open)
             .AddTokenPattern(@"\)", TokenType.close)
             .AddTokenPattern(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real)
             .AddTokenPattern(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer)
             .AddIgnorePattern(@"( |\t)+", TokenType.white_space)
             .End();
      }
   }
}
