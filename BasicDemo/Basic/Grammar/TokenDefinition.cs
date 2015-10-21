using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class TokenDefinition : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.AddToken(@"PROGRAM", TokenType.program_keyword);
         yacc.AddToken(@"END", TokenType.end_keyword);
         yacc.AddToken(@"\+", TokenType.plus);
         yacc.AddToken(@"\-", TokenType.minus);
         yacc.AddToken(@"\*", TokenType.mult);
         yacc.AddToken(@"\/", TokenType.div);
         yacc.AddToken(@"=", TokenType.equal);
         yacc.AddToken(@"\>", TokenType.greater);
         yacc.AddToken(@"\>=", TokenType.greaterEqual);
         yacc.AddToken(@"\<", TokenType.less);
         yacc.AddToken(@"\<=", TokenType.lessEqual);
         yacc.AddToken(@"\(", TokenType.open);
         yacc.AddToken(@"\)", TokenType.close);
         yacc.AddToken(@",", TokenType.comma);
         yacc.AddToken(@":", TokenType.colon);
         yacc.AddToken("\r\n", TokenType.Return);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         yacc.AddToken("\".*\"", TokenType.text);
         yacc.AddToken(@"DIM", TokenType.dim_keyword);
         yacc.AddToken(@"LET", TokenType.let_keyword);
         yacc.AddToken(@"IF", TokenType.if_keyword);
         yacc.AddToken(@"THEN", TokenType.then_keyword);
         yacc.AddToken(@"ELSE", TokenType.else_keyword);
         yacc.AddToken(@"WHILE", TokenType.while_keyword);
         yacc.AddToken(@"FOR", TokenType.for_keyword);
         yacc.AddToken(@"TO", TokenType.to_keyword);
         yacc.AddToken(@"STEP", TokenType.step_keyword);
         yacc.AddToken(@"DO", TokenType.do_keyword);
         yacc.AddToken(@"GOTO", TokenType.goto_keyword);
         yacc.AddToken(@"PLOT", TokenType.plot_keyword);
         yacc.AddToken(null, TokenType.label);
         yacc.AddToken(@"(\w)+(\w|\d)*", TokenType.name);
         yacc.AddTokenToIgnore(@"( )+", TokenType.white_space);
      }
   }
}
