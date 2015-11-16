using LexSharp;

namespace ScanningBasicDemo
{
   public static class TokenDefinition
   {
      public static void Define(this LexSharp<TokenType> lex)
      {
         lex.Register(@"PROGRAM", TokenType.program_keyword);
         lex.Register(@"END", TokenType.end_keyword);
         lex.Register(@"\+", TokenType.plus);
         lex.Register(@"\-", TokenType.minus);
         lex.Register(@"\*", TokenType.mult);
         lex.Register(@"\/", TokenType.div);
         lex.Register(@"=", TokenType.equal);
         lex.Register(@"\>", TokenType.greater);
         lex.Register(@"\>=", TokenType.greaterEqual);
         lex.Register(@"\<", TokenType.less);
         lex.Register(@"\<=", TokenType.lessEqual);
         lex.Register(@"\(", TokenType.open);
         lex.Register(@"\)", TokenType.close);
         lex.Register(@",", TokenType.comma);
         lex.Register(@":", TokenType.colon);
         lex.Register("\r\n", TokenType.Return);
         lex.Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         lex.Register(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         lex.Register("\".*\"", TokenType.text);
         lex.Register(@"DIM", TokenType.dim_keyword);
         lex.Register(@"LET", TokenType.let_keyword);
         lex.Register(@"IF", TokenType.if_keyword);
         lex.Register(@"THEN", TokenType.then_keyword);
         lex.Register(@"ELSE", TokenType.else_keyword);
         lex.Register(@"WHILE", TokenType.while_keyword);
         lex.Register(@"FOR", TokenType.for_keyword);
         lex.Register(@"TO", TokenType.to_keyword);
         lex.Register(@"STEP", TokenType.step_keyword);
         lex.Register(@"DO", TokenType.do_keyword);
         lex.Register(@"GOTO", TokenType.goto_keyword);
         lex.Register(@"PLOT", TokenType.plot_keyword);
         lex.Register(@"NOT", TokenType.not_keyword);
         lex.Register(@"AND", TokenType.and_keyword);
         lex.Register(@"OR", TokenType.or_keyword);
         lex.Register(@"MOD", TokenType.mod_keyword);
         lex.Register(null, TokenType.label);
         lex.Register(@"(\w)+(\w|\d)*", TokenType.name);
         lex.RegisterIgnorePattern(@"( |\t)+", TokenType.white_space);
      }
   }
}
