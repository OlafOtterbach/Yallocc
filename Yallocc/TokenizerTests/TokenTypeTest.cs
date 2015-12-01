using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public abstract class TokenTypeTest
   {
      protected abstract TokenizerCreator<TokenType> GetCreator();

      [TestMethod]
      public void ScanTest_WhenHallo_ThenTokenIsName()
      {
         var tokenizer = CreateTokenizer();

         var name = tokenizer.Scan("Hallo").ToList();

         Assert.IsTrue(name.Any());
         Assert.AreEqual(name.First().Type, TokenType.name);
         Assert.AreEqual(name.First().Value, "Hallo");
      }

      [TestMethod]
      public void SpecialCharactersTest_WhenSpecialCharacters_ThenRecognizingTokenType()
      {
         var tokenizer = CreateTokenizer();

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
      public void NumberTest_WhenNumber_ThenRecognizingNumber()
      {
         var tokenizer = CreateTokenizer();

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
         var creator = GetCreator();

         creator.Register(@"\+", TokenType.plus);
         creator.Register(@"\-", TokenType.minus);
         creator.Register(@"\*", TokenType.mult);
         creator.Register(@"\/", TokenType.div);
         creator.Register(@"=", TokenType.equal);
         creator.Register(@"\>", TokenType.greater);
         creator.Register(@"\<", TokenType.less);
         creator.Register(@"\(", TokenType.open);
         creator.Register(@"\)", TokenType.close);
         creator.Register(@"(0|1|2|3|4|5|6|7|8|9)*.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         creator.Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         creator.Register(@"(\w)+", TokenType.name);

         return creator.Create();
      }

      public enum TokenType
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
