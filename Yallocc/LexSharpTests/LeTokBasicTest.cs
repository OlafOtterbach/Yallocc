using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LexSharp
{
   [TestClass]
   public class LeTokBasicTest
   {
      [TestMethod]
      public void Test_WhenGiantRandomBasicText_ThenAllTokensDetected()
      {
         var lex = CreateLex();
         var text = CreateBasicText(10000);

         var sequence = lex.Scan(text);

         Assert.IsTrue(sequence.All(tok => tok.IsValid));
      }

      [TestMethod]
      public void ScanText_WhenTestBasicText_ThenAllTokensDetected()
      {
         var lex = CreateLex();
         var text = "Name , ) MOD LET";

         var sequence = lex.Scan(text).ToList();

         Assert.IsTrue(sequence.All(tok => tok.IsValid));
      }

      [TestMethod]
      public void ScanText_WhenRandomBasicText_ThenAllTokensDetected()
      {
         var lex = CreateLex();
         var text = CreateBasicText(5);

         var sequence = lex.Scan(text).ToList();

         Assert.IsTrue(sequence.All(tok => tok.IsValid));
      }

      [TestMethod]
      public void ScanText_WhenGiantRandomBasicText_ThenAllTokensDetected()
      {
         var lex = CreateLex();
         var text = CreateBasicText(10000);

         var sequence = lex.Scan(text).ToList();

         Assert.IsTrue(sequence.All(tok => tok.IsValid));
      }

      private enum TokenType
      {
         program_keyword, // PROGRAM
         end_keyword,     // END
         plus,            // +
         minus,           // -
         mult,            // *
         div,             // /
         equal,           // =
         greater,         // >
         greaterEqual,    // >=
         less,            // <
         lessEqual,       // <=
         open,            // (
         close,           // )
         comma,           // ,
         colon,           // :
         Return,          // \n
         integer,         // 1, 2, 3, 12, 123, ...
         real,            // 1.0, 12.0, 1.0, 0.2, .4, ...
         text,            // "Hallo", ...
         and_keyword,     // AND
         or_keyword,      // OR
         not_keyword,     // NOT
         mod_keyword,     // MOD
         dim_keyword,     // DIM
         let_keyword,     // LET
         if_keyword,      // IF
         then_keyword,    // THEN
         else_keyword,    // ELSE
         while_keyword,   // WHILE
         for_keyword,     // FOR
         to_keyword,      // TO
         step_keyword,    // STEP
         do_keyword,      // DO
         goto_keyword,    // GOTO
         plot_keyword,    // PLOT
         label,           // Label:
         name,            // x, y, index, ...
         white_space      // _, TAB
      }

      private static LeTok<TokenType> CreateLex()
      {
         var lex = TokenizerBuilder<TokenType>
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
            //         lex.Register(null, TokenType.label)
            .Register(@"(\w)+(\w|\d)*", TokenType.name)
            .RegisterIgnorePattern(@"( |\t)+", TokenType.white_space)
            .Init();
         return lex;
      }

      static string CreateBasicText(int limit)
      {
         var words = new System.Collections.Generic.List<string>
         {
            "PROGRAM",
            "END",
            "+",
            "-",
            "*",
            "/",
            "=",
            ">",
            ">=",
            "<",
            "<=",
            "(",
            ")",
            ",",
            ":",
            "\r\n",
            "1234",
            "123.456",
            "Text",
            "DIM",
            "LET",
            "IF",
            "THEN",
            "ELSE",
            "WHILE",
            "FOR",
            "TO",
            "STEP",
            "DO",
            "GOTO",
            "PLOT",
            "NOT",
            "AND",
            "OR",
            "MOD",
            "Name"
         };

         var rand = new Random();
         var text = Enumerable.Range(0, limit)
                              .Select(i => words[rand.Next(0, words.Count)])
                              .Aggregate((current, elem) => current = current + " " + elem);
         return text;
      }
   }
}