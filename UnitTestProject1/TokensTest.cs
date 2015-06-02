using System.Linq;
using LexSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YalloccDemo;

namespace YalloccDemoTest
{
   [TestClass]
   public class TokensTest
   {
      [TestMethod]
      public void SpecialCharactersTest_SpecialCharacters_RecognizingTokens()
      {
         LexSharp<Tokens> lex = new LexSharp<Tokens>();
         DefineTokens(lex);

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

         Assert.AreEqual(plus.First().Type, Tokens.plus);
         Assert.AreEqual(minus.First().Type, Tokens.minus);
         Assert.AreEqual(mult.First().Type, Tokens.mult);
         Assert.AreEqual(div.First().Type, Tokens.div);
         Assert.AreEqual(equal.First().Type, Tokens.equal);
         Assert.AreEqual(greater.First().Type, Tokens.greater);
         Assert.AreEqual(less.First().Type, Tokens.less);
         Assert.AreEqual(open.First().Type, Tokens.open);
         Assert.AreEqual(close.First().Type, Tokens.close);

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
         LexSharp<Tokens> lex = new LexSharp<Tokens>();
         DefineTokens(lex);

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

         Assert.AreEqual(one.First().Type, Tokens.number);
         Assert.AreEqual(oneDot.First().Type, Tokens.number);
         Assert.IsFalse(oneDot.Last().IsValid);
         Assert.AreEqual(oneDotZeroOneTwoThree.First().Type, Tokens.number);
         Assert.AreEqual(zero.First().Type, Tokens.number);
         Assert.AreEqual(zeroDot.First().Type, Tokens.number);
         Assert.IsFalse(dotZero.First().IsValid);
         Assert.AreEqual(zeroDotOneTwoThree.First().Type, Tokens.number);

         Assert.AreEqual(one.First().Value, "1");
         Assert.AreEqual(oneDot.First().Value, "1");
         Assert.AreEqual(oneDotZeroOneTwoThree.First().Value, "1.0123");
         Assert.AreEqual(zero.First().Value, "0");
         Assert.AreEqual(zeroDot.First().Value, "0");
         Assert.AreEqual(dotZero.Last().Value, "0");
         Assert.AreEqual(zeroDotOneTwoThree.First().Value, "0.123");
      }

      private void DefineTokens(LexSharp<Tokens> lex)
      {
         lex.Register(@"\+", Tokens.plus);
         lex.Register(@"\-", Tokens.minus);
         lex.Register(@"\*", Tokens.mult);
         lex.Register(@"\/", Tokens.div);
         lex.Register(@"=", Tokens.equal);
         lex.Register(@"\>", Tokens.greater);
         lex.Register(@"\<", Tokens.less);
         lex.Register(@"\(", Tokens.open);
         lex.Register(@"\)", Tokens.close);
         lex.Register(@"(0|1|2|3|4|5|6|7|8|9)+(\.(0|1|2|3|4|5|6|7|8|9)+)?", Tokens.number);
         lex.Register(@"\w", Tokens.name);
      }

   }
}
