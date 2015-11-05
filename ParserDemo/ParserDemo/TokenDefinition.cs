using Yallocc;

namespace ParserDemo
{
   public static class TokenDefinition
   {
      public static void DefineTokens(this Yallocc<TokenType> yacc)
      {
         yacc.AddToken(@"\+", TokenType.plus);
         yacc.AddToken(@"\-", TokenType.minus);
         yacc.AddToken(@"\*", TokenType.mult);
         yacc.AddToken(@"\/", TokenType.div);
         yacc.AddToken(@"\(", TokenType.open);
         yacc.AddToken(@"\)", TokenType.close);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         yacc.AddTokenToIgnore(@"( |\t)+", TokenType.white_space);
      }
   }
}
