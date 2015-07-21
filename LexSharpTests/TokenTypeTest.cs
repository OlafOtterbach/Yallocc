using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace LexSharp
{
   [TestClass]
   public class TokenTypeTest
   {
      [TestMethod]
      public void TextTest()
      {
         var pattern = "\".*\"";
         var text = "\"Hallo\" \"Test\"";
         var matches = Regex.Match(text, pattern);
         var captures = matches.Captures;
      }

      [TestMethod]
      public void NameTest()
      {
         LexSharp<TokenType> lex = new LexSharp<TokenType>();
         DefineTokenType(lex);

         var name = lex.Scan("Hallo").ToList();

         Assert.IsTrue(name.Any());
         Assert.AreEqual(name.First().Type, TokenType.name);
         Assert.AreEqual(name.First().Value, "Hallo");
      }

      [TestMethod]
      public void SpecialCharactersTest_SpecialCharacters_RecognizingTokenType()
      {
         LexSharp<TokenType> lex = new LexSharp<TokenType>();
         DefineTokenType(lex);

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
         LexSharp<TokenType> lex = new LexSharp<TokenType>();
         DefineTokenType(lex);

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

      private void DefineTokenType(LexSharp<TokenType> lex)
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
         lex.Register(@"(\w)+", TokenType.name);
      }

      private enum TokenType
      {
         plus,          // +
         minus,         // -
         mult,          // *
         div,           // /
         equal,         // =
         greater,       // >
         less,          // <
         open,          // (
         close,         // )
         open_clamp,    // [
         close_clamp,   // }
         integer,       // 1, 2, 3, 12, 123, ...
         real,          // 1.0, 12.0, 1.0, 0.2, .4, ...
         text,          // "Hallo", ...
         dim,           // DIM
         let,           // LET
         name,          // x, y, index, ...
      }
   }
}
