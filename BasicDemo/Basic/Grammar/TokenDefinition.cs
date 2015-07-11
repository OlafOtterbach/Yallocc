using Yallocc;

namespace BasicDemo.Basic
{
   public class TokenDefinition
   {
      public void DefineExpressionTokens(Yallocc<TokenType> yacc)
      {
         yacc.AddToken(@"\+", TokenType.plus);
         yacc.AddToken(@"\-", TokenType.minus);
         yacc.AddToken(@"\*", TokenType.mult);
         yacc.AddToken(@"\/", TokenType.div);
         yacc.AddToken(@"=", TokenType.equal);
         yacc.AddToken(@"\>", TokenType.greater);
         yacc.AddToken(@"\<", TokenType.less);
         yacc.AddToken(@"\(", TokenType.open);
         yacc.AddToken(@"\)", TokenType.close);
         yacc.AddToken(@"\[", TokenType.open_clamp);
         yacc.AddToken(@"\]", TokenType.close_clamp);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         yacc.AddToken("\"\\w\"", TokenType.text);
         yacc.AddToken(@"DIM", TokenType.dim);
         yacc.AddToken(@"LET", TokenType.let);
         yacc.AddToken(@"\w", TokenType.name);
      }
   }
}
