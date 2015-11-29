﻿using System.Linq;
using LexSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicDemo;
using BasicDemo.Basic;
using System.Text.RegularExpressions;
using Yallocc;
using System.IO;
using SyntaxTree;

namespace BasicDemoTest
{
   [TestClass]
   public class TokenTypeTest
   {
      [TestMethod]
      public void ColonTest_OneColon_Colon()
      {
         var yacc = new Yallocc<TokenType>();
         var builder = new SyntaxTreeBuilder();
         var tokenDefinition = new TokenDefinition();
         tokenDefinition.Define(yacc, null);
         var lex = yacc.Lex;
         lex.Initialize();

         var tokens = lex.Scan("456:123").ToList();

         Assert.IsTrue(tokens.Any());
         Assert.AreEqual(tokens.First().Type, TokenType.integer);
         Assert.AreEqual(tokens[1].Type, TokenType.colon);
         Assert.AreEqual(tokens[2].Type, TokenType.integer);
      }

      [TestMethod]
      public void NameTest_OneName_SyntaxTreeBuilderDoesNotInfluenceResult()
      {
         var yacc1 = new Yallocc<TokenType>();
         var yacc2 = new Yallocc<TokenType>();
         var builder = new SyntaxTreeBuilder();
         var tokenDefinition = new TokenDefinition();
         tokenDefinition.Define(yacc1, null);
         tokenDefinition.Define(yacc2, null);
         var lex1 = yacc1.Lex;
         var lex2 = yacc1.Lex;
         lex1.Initialize();
         lex2.Initialize();

         var tokens1 = lex1.Scan("Hallo").ToList();
         var tokens2 = lex2.Scan("Hallo").ToList();

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
         var yacc = new Yallocc<TokenType>();
         var tokenDefinition = new TokenDefinition();
         tokenDefinition.Define(yacc, null);
         var lex = yacc.Lex;
         lex.Initialize();

         var tokens1 = lex.Scan("Hallo").ToList();

         Assert.IsTrue(tokens1.Any());
         Assert.AreEqual(tokens1.First().Type, TokenType.name);
         Assert.AreEqual(tokens1.First().Value, "Hallo");

         var tokens2 = lex.Scan("PROGRAM \"text\"\r\nLET a = 2").ToList();
      }

      [TestMethod]
      public void NameTest_ProgramText_NameScanned()
      {
         var yacc = new Yallocc<TokenType>();
         var tokenDefinition = new TokenDefinition();
         tokenDefinition.Define(yacc, null);
         var lex = yacc.Lex;
         lex.Initialize();

         var text = "PROGRAM \"LET Statement\"\r\nLET a";
         var tokens = lex.Scan(text).ToList();

         Assert.IsTrue(tokens.Any());
         Assert.AreEqual(tokens[4].Type, TokenType.name);
         Assert.AreEqual(tokens[4].Value, "a");
      }

      [TestMethod]
      public void ScannerStressTest()
      {
         var yacc = new Yallocc<TokenType>();
         var tokenDefinition = new TokenDefinition();
         tokenDefinition.Define(yacc, null);
         var lex = yacc.Lex;
         lex.Initialize();

         var text = "\"LET Statement\" LET a";
         var tokens = lex.Scan(text).ToList();

         Assert.IsTrue(tokens.Any());
         Assert.AreEqual(tokens[2].Type, TokenType.name);
         Assert.AreEqual(tokens[2].Value, "a");
      }

      [TestMethod]
      public void Name_FileAsInput_NameScanned()
      {
         var yacc = new Yallocc<TokenType>();
         var tokenDefinition = new TokenDefinition();
         tokenDefinition.Define(yacc, null);
         var lex = yacc.Lex;
         lex.Initialize();

         var programText = File.ReadAllText(@"Basic\Grammar\TestData\LetStatementProgram.basic");
         var tokens = lex.Scan(programText).ToList();

         Assert.IsTrue(tokens.Any());
         Assert.AreEqual(tokens[4].Type, TokenType.name);
         Assert.AreEqual(tokens[4].Value, "a");
      }


      [TestMethod]
      public void SpecialCharactersTest_SpecialCharacters_RecognizingTokenType()
      {
         LeTok<TokenType> lex = new LeTok<TokenType>();
         DefineTokenType(lex);
         lex.Initialize();

         var plus = lex.Scan("+").ToList();
         var minus = lex.Scan("-").ToList();
         var mult = lex.Scan("*").ToList();
         var div = lex.Scan("/").ToList();
         var equal = lex.Scan("=").ToList();
         var greater = lex.Scan(">").ToList();
         var less = lex.Scan("<").ToList();
         var open = lex.Scan("(").ToList();
         var close = lex.Scan(")").ToList();

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
         LeTok<TokenType> lex = new LeTok<TokenType>();

         var one = lex.Scan("1").ToList();
         var oneDot = lex.Scan("1.").ToList();
         var oneDotZeroOneTwoThree = lex.Scan("1.0123").ToList();
         var zero = lex.Scan("0").ToList();
         var zeroDot = lex.Scan("0.").ToList();
         var dotZero = lex.Scan(".0").ToList();
         var zeroDotOneTwoThree = lex.Scan("0.123").ToList();

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

      private void DefineTokenType(LeTok<TokenType> lex)
      {
         lex.Register(@"\+", TokenType.plus);
         lex.Register(@"\-", TokenType.minus);
         lex.Register(@"\*", TokenType.mult);
         lex.Register(@"\/", TokenType.div);
         lex.Register(@"=", TokenType.equal);
         lex.Register(@"\>", TokenType.greater);
         lex.Register(@"\<", TokenType.less);
         lex.Register(@"\(", TokenType.open);
         lex.Register(@"\)", TokenType.close);
         lex.Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         lex.Register(@"(0|1|2|3|4|5|6|7|8|9)*.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         lex.Initialize();
      }
   }
}
