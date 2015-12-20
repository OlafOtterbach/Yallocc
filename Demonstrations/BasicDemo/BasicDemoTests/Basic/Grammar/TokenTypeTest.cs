using System.Linq;
using Yallocc.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicDemo;
using BasicDemo.Basic;
using System.Text.RegularExpressions;
using Yallocc;
using System.IO;
using SyntaxTree;
using Yallocc.Tokenizer.LeTok;
using Yallocc.Tokenizer.LexSharp;

namespace BasicDemoTest
{
   [TestClass]
   public class TokenTypeTest
   {
      [TestMethod]
      public void ColonTest_OneColon_Colon()
      {
         var tokenizer = CreateBasicTokenizer();

         var tokens = tokenizer.Scan("456:123").ToList();

         Assert.IsTrue(tokens.Any());
         Assert.AreEqual(tokens.First().Type, TokenType.integer);
         Assert.AreEqual(tokens[1].Type, TokenType.colon);
         Assert.AreEqual(tokens[2].Type, TokenType.integer);
      }

      [TestMethod]
      public void NameTest_OneName_SyntaxTreeBuilderDoesNotInfluenceResult()
      {
         var tokenizer1 = CreateBasicTokenizer();
         var tokenizer2 = CreateBasicTokenizer();

         var tokens1 = tokenizer1.Scan("Hallo").ToList();
         var tokens2 = tokenizer2.Scan("Hallo").ToList();

         Assert.IsTrue(tokens1.Any());
         Assert.AreEqual(tokens1.First().Type, TokenType.name);
         Assert.AreEqual(tokens1.First().Value, "Hallo");
         Assert.IsTrue(tokens2.Any());
         Assert.AreEqual(tokens2.First().Type, TokenType.name);
         Assert.AreEqual(tokens2.First().Value, "Hallo");
      }


      [TestMethod]
      public void NameTest_OneName_NameScanned()
      {
         var tokenizer = CreateBasicTokenizer();

         var tokens1 = tokenizer.Scan("Hallo").ToList();

         Assert.IsTrue(tokens1.Any());
         Assert.AreEqual(tokens1.First().Type, TokenType.name);
         Assert.AreEqual(tokens1.First().Value, "Hallo");

         var tokens2 = tokenizer.Scan("PROGRAM \"text\"\r\nLET a = 2").ToList();
      }

      [TestMethod]
      public void NameTest_ProgramText_NameScanned()
      {
         var tokenizer = CreateBasicTokenizer();

         var text = "PROGRAM \"LET Statement\"\r\nLET a";
         var tokens = tokenizer.Scan(text).ToList();

         Assert.IsTrue(tokens.Any());
         Assert.AreEqual(tokens[4].Type, TokenType.name);
         Assert.AreEqual(tokens[4].Value, "a");
      }

      [TestMethod]
      public void ScannerStressTest()
      {
         var tokenizer = CreateBasicTokenizer();

         var text = "\"LET Statement\" LET a";
         var tokens = tokenizer.Scan(text).ToList();

         Assert.IsTrue(tokens.Any());
         Assert.AreEqual(tokens[2].Type, TokenType.name);
         Assert.AreEqual(tokens[2].Value, "a");
      }

      [TestMethod]
      public void Name_FileAsInput_NameScanned()
      {
         var tokenizer = CreateBasicTokenizer();

         var programText = File.ReadAllText(@"Basic\Grammar\TestData\LetStatementProgram.basic");
         var tokens = tokenizer.Scan(programText).ToList();

         Assert.IsTrue(tokens.Any());
         Assert.AreEqual(tokens[4].Type, TokenType.name);
         Assert.AreEqual(tokens[4].Value, "a");
      }

      [TestMethod]
      public void SpecialCharactersTest_SpecialCharacters_RecognizingTokenType()
      {
         Tokenizer<TokenType> tokenizer = CreateTokenizer();

         var plus = tokenizer.Scan("+").ToList();
         var minus = tokenizer.Scan("-").ToList();
         var mult = tokenizer.Scan("*").ToList();
         var div = tokenizer.Scan("/").ToList();
         var equal = tokenizer.Scan("=").ToList();
         var greater = tokenizer.Scan(">").ToList();
         var less = tokenizer.Scan("<").ToList();
         var open = tokenizer.Scan("(").ToList();
         var close = tokenizer.Scan(")").ToList();

         Assert.IsTrue(plus.Any());
         Assert.IsTrue(minus.Any());
         Assert.IsTrue(mult.Any());
         Assert.IsTrue(div.Any());
         Assert.IsTrue(equal.Any());
         Assert.IsTrue(greater.Any());
         Assert.IsTrue(less.Any());
         Assert.IsTrue(open.Any());
         Assert.IsTrue(close.Any());

         Assert.AreEqual(plus.First().Type, TokenType.plus);
         Assert.AreEqual(minus.First().Type, TokenType.minus);
         Assert.AreEqual(mult.First().Type, TokenType.mult);
         Assert.AreEqual(div.First().Type, TokenType.div);
         Assert.AreEqual(equal.First().Type, TokenType.equal);
         Assert.AreEqual(greater.First().Type, TokenType.greater);
         Assert.AreEqual(less.First().Type, TokenType.less);
         Assert.AreEqual(open.First().Type, TokenType.open);
         Assert.AreEqual(close.First().Type, TokenType.close);

         Assert.AreEqual(plus.First().Value, "+");
         Assert.AreEqual(minus.First().Value, "-");
         Assert.AreEqual(mult.First().Value, "*");
         Assert.AreEqual(div.First().Value, "/");
         Assert.AreEqual(equal.First().Value, "=");
         Assert.AreEqual(greater.First().Value, ">");
         Assert.AreEqual(less.First().Value, "<");
         Assert.AreEqual(open.First().Value, "(");
         Assert.AreEqual(close.First().Value, ")");
      }

      [TestMethod]
      public void NumberTest_Number_RecognizingNumber()
      {
         Tokenizer<TokenType> tokenizer = CreateTokenizer();

         var one = tokenizer.Scan("1").ToList();
         var oneDot = tokenizer.Scan("1.").ToList();
         var oneDotZeroOneTwoThree = tokenizer.Scan("1.0123").ToList();
         var zero = tokenizer.Scan("0").ToList();
         var zeroDot = tokenizer.Scan("0.").ToList();
         var dotZero = tokenizer.Scan(".0").ToList();
         var zeroDotOneTwoThree = tokenizer.Scan("0.123").ToList();

         Assert.IsTrue(one.Any());
         Assert.IsTrue(oneDot.Any());
         Assert.IsTrue(oneDotZeroOneTwoThree.Any());
         Assert.IsTrue(zero.Any());
         Assert.IsTrue(zeroDot.Any());
         Assert.IsTrue(dotZero.Any());
         Assert.IsTrue(zeroDotOneTwoThree.Any());

         Assert.AreEqual(one.First().Type, TokenType.integer);
         Assert.AreEqual(oneDot.First().Type, TokenType.integer);
         Assert.IsFalse(oneDot.Last().IsValid);
         Assert.AreEqual(oneDotZeroOneTwoThree.First().Type, TokenType.real);
         Assert.AreEqual(zero.First().Type, TokenType.integer);
         Assert.AreEqual(zeroDot.First().Type, TokenType.integer);
         Assert.IsTrue(dotZero.Last().IsValid);
         Assert.AreEqual(zeroDotOneTwoThree.First().Type, TokenType.real);

         Assert.AreEqual(one.First().Value, "1");
         Assert.AreEqual(oneDot.First().Value, "1");
         Assert.AreEqual(oneDotZeroOneTwoThree.First().Value, "1.0123");
         Assert.AreEqual(zero.First().Value, "0");
         Assert.AreEqual(zeroDot.First().Value, "0");
         Assert.AreEqual(dotZero.Last().Value, ".0");
         Assert.AreEqual(zeroDotOneTwoThree.First().Value, "0.123");
      }

      private Tokenizer<TokenType> CreateTokenizer()
      {
         var tokenizerCreator = new LexSharpCreator<TokenType>();
         tokenizerCreator.Register(@"\+", TokenType.plus);
         tokenizerCreator.Register(@"\-", TokenType.minus);
         tokenizerCreator.Register(@"\*", TokenType.mult);
         tokenizerCreator.Register(@"\/", TokenType.div);
         tokenizerCreator.Register(@"=", TokenType.equal);
         tokenizerCreator.Register(@"\>", TokenType.greater);
         tokenizerCreator.Register(@"\<", TokenType.less);
         tokenizerCreator.Register(@"\(", TokenType.open);
         tokenizerCreator.Register(@"\)", TokenType.close);
         tokenizerCreator.Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         tokenizerCreator.Register(@"(0|1|2|3|4|5|6|7|8|9)*.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         var tokenizer = tokenizerCreator.Create();
         return tokenizer;
      }

      private Tokenizer<TokenType> CreateBasicTokenizer()
      {
         var tokenizerCreator = new LeTokCreator<TokenType>();
         var yacc = new Yallocc<SyntaxTreeBuilder, TokenType>(tokenizerCreator);
         var tokenDefinition = new TokenDefinition();
         tokenDefinition.Define(yacc);
         yacc.MasterGrammar("Dummy").Enter.Exit.EndGrammar();
         var parserAndTokenizer = yacc.CreateParser();
         var tokenizer = parserAndTokenizer.Tokenizer;
         return tokenizer;
      }
   }
}
