using LexSharp;

namespace ScanningBasicDemo
{
   public static class TokenDefinition
   {
      public static ITokenizer<TokenType> CreateTokenizer()
      {
         ITokenizer<TokenType> lex =LeTokBuilder<TokenType>
            .Create()
            .Register(@"PROGRAM", TokenType.program_keyword)
            .Register(@"END", TokenType.end_keyword)
            .Register(@"\+", TokenType.plus)
            .Register(@"\-", TokenType.minus)
            .Register(@"\*", TokenType.mult)
            .Register(@"\/", TokenType.div)
            .Register(@"=", TokenType.equal)
            .Register(@"\>", TokenType.greater)
            .Register(@"\>=", TokenType.greaterEqual)
            .Register(@"\<", TokenType.less)
            .Register(@"\<=", TokenType.lessEqual)
            .Register(@"\(", TokenType.open)
            .Register(@"\)", TokenType.close)
            .Register(@",", TokenType.comma)
            .Register(@":", TokenType.colon)
            .Register("\r\n", TokenType.Return)
            .Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer)
            .Register(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real)
            .Register("\".*\"", TokenType.text)
            .Register(@"DIM", TokenType.dim_keyword)
            .Register(@"LET", TokenType.let_keyword)
            .Register(@"IF", TokenType.if_keyword)
            .Register(@"THEN", TokenType.then_keyword)
            .Register(@"ELSE", TokenType.else_keyword)
            .Register(@"WHILE", TokenType.while_keyword)
            .Register(@"FOR", TokenType.for_keyword)
            .Register(@"TO", TokenType.to_keyword)
            .Register(@"STEP", TokenType.step_keyword)
            .Register(@"DO", TokenType.do_keyword)
            .Register(@"GOTO", TokenType.goto_keyword)
            .Register(@"PLOT", TokenType.plot_keyword)
            .Register(@"NOT", TokenType.not_keyword)
            .Register(@"AND", TokenType.and_keyword)
            .Register(@"OR", TokenType.or_keyword)
            .Register(@"MOD", TokenType.mod_keyword)
            .Register(null, TokenType.label)
            .Register(@"(\w)+(\w|\d)*", TokenType.name)
            .RegisterIgnorePattern(@"( |\t)+", TokenType.white_space)
            .Initialize();
         return lex;
      }
   }
}
