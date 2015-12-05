using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public abstract class BasicTest
   {
      protected abstract TokenizerCreator<TokenType> GetCreator();

      [TestMethod]
      public void ScanText_WhenTestBasicText_ThenAllTokensDetected()
      {
         var tokenizer = CreateTokenizer();
         var text = "Name , ) MOD LET";

         var sequence = tokenizer.Scan(text).ToList();

         Assert.IsTrue(sequence.All(tok => tok.IsValid));
      }

      [TestMethod]
      public void ScanTest_WhenGiantRandomBasicText_ThenAllTokensDetected()
      {
         var tokenizer = CreateTokenizer();
         var text = CreateBasicText(10000);

         var sequence = tokenizer.Scan(text);

         Assert.IsTrue(sequence.All(tok => tok.IsValid));
      }

      [TestMethod]
      public void ScanTest_WhenRandomBasicText_ThenAllTokensDetected()
      {
         var tokenizer = CreateTokenizer();
         var text = CreateBasicText(5);

         var sequence = tokenizer.Scan(text).ToList();

         Assert.IsTrue(sequence.All(tok => tok.IsValid));
      }

      [TestMethod]
      public void ParallelScanText_WhenScanSameTextParallel_ThenAllResultsAreEqual()
      {
         var tokenizer = CreateTokenizer();
         var text = CreateBasicText(10000);


         var sequences = Enumerable.Range(0, 8).AsParallel().Select(i => tokenizer.Scan(text).ToList()).ToList();
         var allHaveSameLength = Enumerable.Range(0, 7)
                                           .Select(i => new { Curr = i, Succ = i + 1 })
                                           .All(p => sequences[p.Curr].Count == sequences[p.Succ].Count);
         Assert.IsTrue(allHaveSameLength);
         var allAreEqual = Enumerable.Range(0, 7)
                                     .Select(i => new { Curr = i, Succ = i + 1 })
                                     .Select(p => sequences[p.Curr].Zip(sequences[p.Succ], (c, s) => c.Type == s.Type))
                                     .All(checks => checks.All(x => x));
         Assert.IsTrue(allAreEqual);
      }

      protected enum TokenType
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

      protected Tokenizer<TokenType> CreateTokenizer()
      {
         var creator = GetCreator();
         creator.Register(@"PROGRAM", TokenType.program_keyword);
         creator.Register(@"END", TokenType.end_keyword);
         creator.Register(@"\+", TokenType.plus);
         creator.Register(@"\-", TokenType.minus);
         creator.Register(@"\*", TokenType.mult);
         creator.Register(@"\/", TokenType.div);
         creator.Register(@"=", TokenType.equal);
         creator.Register(@"\>", TokenType.greater);
         creator.Register(@"\>=", TokenType.greaterEqual);
         creator.Register(@"\<", TokenType.less);
         creator.Register(@"\<=", TokenType.lessEqual);
         creator.Register(@"\(", TokenType.open);
         creator.Register(@"\)", TokenType.close);
         creator.Register(@",", TokenType.comma);
         creator.Register(@":", TokenType.colon);
         creator.Register("\r\n", TokenType.Return);
         creator.Register(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         creator.Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         creator.Register("\".*\"", TokenType.text);
         creator.Register(@"DIM", TokenType.dim_keyword);
         creator.Register(@"LET", TokenType.let_keyword);
         creator.Register(@"IF", TokenType.if_keyword);
         creator.Register(@"THEN", TokenType.then_keyword);
         creator.Register(@"ELSE", TokenType.else_keyword);
         creator.Register(@"WHILE", TokenType.while_keyword);
         creator.Register(@"FOR", TokenType.for_keyword);
         creator.Register(@"TO", TokenType.to_keyword);
         creator.Register(@"STEP", TokenType.step_keyword);
         creator.Register(@"DO", TokenType.do_keyword);
         creator.Register(@"GOTO", TokenType.goto_keyword);
         creator.Register(@"PLOT", TokenType.plot_keyword);
         creator.Register(@"NOT", TokenType.not_keyword);
         creator.Register(@"AND", TokenType.and_keyword);
         creator.Register(@"OR", TokenType.or_keyword);
         creator.Register(@"MOD", TokenType.mod_keyword);
         creator.Register(@"(\w)+(\w|\d)*", TokenType.name);
         creator.RegisterIgnorePattern(@"( |\t)+", TokenType.white_space);
         return creator.Create();
      }

      protected static string CreateBasicText(int limit)
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