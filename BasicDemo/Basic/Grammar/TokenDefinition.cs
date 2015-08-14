using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class TokenDefinition : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.AddToken(@"PROGRAM", TokenType.program);
         yacc.AddToken(@"\+", TokenType.plus);
         yacc.AddToken(@"\-", TokenType.minus);
         yacc.AddToken(@"\*", TokenType.mult);
         yacc.AddToken(@"\/", TokenType.div);
         yacc.AddToken(@"=", TokenType.equal);
         yacc.AddToken(@"\>", TokenType.greater);
         yacc.AddToken(@"\<", TokenType.less);
         yacc.AddToken(@"\(", TokenType.open);
         yacc.AddToken(@"\)", TokenType.close);
         yacc.AddToken(@",", TokenType.comma);
         yacc.AddToken("\r\n", TokenType.Return);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         yacc.AddToken("\".*\"", TokenType.text);
         yacc.AddToken(@"DIM", TokenType.dim);
         yacc.AddToken(@"LET", TokenType.let);
         yacc.AddToken(@"(\w)+(\w|\d)*", TokenType.name);
         yacc.AddTokenToIgnore(@"( )+", TokenType.white_space);
      }
   }
}
