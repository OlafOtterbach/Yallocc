using Yallocc.SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class TokenDefinition : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         yacc.DefineTokens()
             .AddTokenPattern(@"PROGRAM", TokenType.program_keyword)
             .AddTokenPattern(@"END", TokenType.end_keyword)
             .AddTokenPattern(@"\+", TokenType.plus)
             .AddTokenPattern(@"\-", TokenType.minus)
             .AddTokenPattern(@"\*", TokenType.mult)
             .AddTokenPattern(@"\/", TokenType.div)
             .AddTokenPattern(@"=", TokenType.equal)
             .AddTokenPattern(@"\>", TokenType.greater)
             .AddTokenPattern(@"\>=", TokenType.greaterEqual)
             .AddTokenPattern(@"\<", TokenType.less)
             .AddTokenPattern(@"\<=", TokenType.lessEqual)
             .AddTokenPattern(@"\(", TokenType.open)
             .AddTokenPattern(@"\)", TokenType.close)
             .AddTokenPattern(@",", TokenType.comma)
             .AddTokenPattern(@":", TokenType.colon)
             .AddTokenPattern("\r\n", TokenType.Return)
             .AddTokenPattern(@"(0|1|2|3|4|5|6|7|8|9)+\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real)
             .AddTokenPattern(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer)
             .AddTokenPattern("\".*\"", TokenType.text)
             .AddTokenPattern(@"DIM", TokenType.dim_keyword)
             .AddTokenPattern(@"LET", TokenType.let_keyword)
             .AddTokenPattern(@"IF", TokenType.if_keyword)
             .AddTokenPattern(@"THEN", TokenType.then_keyword)
             .AddTokenPattern(@"ELSE", TokenType.else_keyword)
             .AddTokenPattern(@"WHILE", TokenType.while_keyword)
             .AddTokenPattern(@"FOR", TokenType.for_keyword)
             .AddTokenPattern(@"TO", TokenType.to_keyword)
             .AddTokenPattern(@"STEP", TokenType.step_keyword)
             .AddTokenPattern(@"DO", TokenType.do_keyword)
             .AddTokenPattern(@"GOTO", TokenType.goto_keyword)
             .AddTokenPattern(@"PLOT", TokenType.plot_keyword)
             .AddTokenPattern(@"NOT", TokenType.not_keyword)
             .AddTokenPattern(@"AND", TokenType.and_keyword)
             .AddTokenPattern(@"OR", TokenType.or_keyword)
             .AddTokenPattern(@"MOD", TokenType.mod_keyword)
             .AddTokenPattern(@"(\w)+(\w|\d)*\:", TokenType.label)
             .AddTokenPattern(@"(\w)+(\w|\d)*", TokenType.name)
             .AddIgnorePattern(@"( |\t)+", TokenType.white_space)
             .End();
      }
   }
}
