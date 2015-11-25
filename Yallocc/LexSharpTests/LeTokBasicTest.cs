using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LexSharp
{
   [TestClass]
   public class LeTokBasicTest
   {
      [TestMethod]
      public void Test_GiantRandomText_AllTokensDetected()
      {
         var lex = CreateLex();
         var text = CreateBasicText(10000);

         var sequence = lex.Scan(text);

         Assert.IsTrue(sequence.All(tok => tok.IsValid));
      }

      [TestMethod]
      public void ScanText_TestText_AllTokensDetected()
      {
         var lex = CreateLex();
         var text = "Name , ) MOD LET";

         var sequence = lex.Scan(text).ToList();

         Assert.IsTrue(sequence.All(tok => tok.IsValid));
      }

      [TestMethod]
      public void ScanText_RandomText_AllTokensDetected()
      {
         var lex = CreateLex();
         var text = CreateBasicText(5);

         var sequence = lex.Scan(text).ToList();

         Assert.IsTrue(sequence.All(tok => tok.IsValid));
      }

      [TestMethod]
      public void ScanText_GiantRandomText_AllTokensDetected()
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
         var lex = new LeTok<TokenType>();
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
//         lex.Register(null, TokenType.label);
         lex.Register(@"(\w)+(\w|\d)*", TokenType.name);
         lex.RegisterIgnorePattern(@"( |\t)+", TokenType.white_space);
         lex.Init();
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