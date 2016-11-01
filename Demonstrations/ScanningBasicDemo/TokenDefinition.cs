using Yallocc.Tokenizer;
using Yallocc.Tokenizer.LeTok;

namespace ScanningBasicDemo
{
   public static class TokenDefinition
   {
      public static Tokenizer<TokenType> CreateTokenizer()
      {
         var tokenizerCreator = new LeTokCreator<TokenType>();
         tokenizerCreator.Register(@"PROGRAM", TokenType.program_keyword);
         tokenizerCreator.Register(@"END", TokenType.end_keyword);
         tokenizerCreator.Register(@"\+", TokenType.plus);
         tokenizerCreator.Register(@"\-", TokenType.minus);
         tokenizerCreator.Register(@"\*", TokenType.mult);
         tokenizerCreator.Register(@"\/", TokenType.div);
         tokenizerCreator.Register(@"=", TokenType.equal);
         tokenizerCreator.Register(@"\>", TokenType.greater);
         tokenizerCreator.Register(@"\>=", TokenType.greaterEqual);
         tokenizerCreator.Register(@"\<", TokenType.less);
         tokenizerCreator.Register(@"\<=", TokenType.lessEqual);
         tokenizerCreator.Register(@"\(", TokenType.open);
         tokenizerCreator.Register(@"\)", TokenType.close);
         tokenizerCreator.Register(@",", TokenType.comma);
         tokenizerCreator.Register(@":", TokenType.colon);
         tokenizerCreator.Register("\r\n", TokenType.Return);
         tokenizerCreator.Register(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         tokenizerCreator.Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         tokenizerCreator.Register("\".*\"", TokenType.text);
         tokenizerCreator.Register(@"DIM", TokenType.dim_keyword);
         tokenizerCreator.Register(@"LET", TokenType.let_keyword);
         tokenizerCreator.Register(@"IF", TokenType.if_keyword);
         tokenizerCreator.Register(@"THEN", TokenType.then_keyword);
         tokenizerCreator.Register(@"ELSE", TokenType.else_keyword);
         tokenizerCreator.Register(@"WHILE", TokenType.while_keyword);
         tokenizerCreator.Register(@"FOR", TokenType.for_keyword);
         tokenizerCreator.Register(@"TO", TokenType.to_keyword);
         tokenizerCreator.Register(@"STEP", TokenType.step_keyword);
         tokenizerCreator.Register(@"DO", TokenType.do_keyword);
         tokenizerCreator.Register(@"GOTO", TokenType.goto_keyword);
         tokenizerCreator.Register(@"PLOT", TokenType.plot_keyword);
         tokenizerCreator.Register(@"NOT", TokenType.not_keyword);
         tokenizerCreator.Register(@"AND", TokenType.and_keyword);
         tokenizerCreator.Register(@"OR", TokenType.or_keyword);
         tokenizerCreator.Register(@"MOD", TokenType.mod_keyword);
         tokenizerCreator.Register(@"(\w)+(\w|\d)*\:", TokenType.label);
         tokenizerCreator.Register(@"(\w)+(\w|\d)*", TokenType.name);
         tokenizerCreator.RegisterIgnorePattern(@"( |\t)+", TokenType.white_space);
         var tokenizer = tokenizerCreator.Create();
         return tokenizer;
      }
   }
}
