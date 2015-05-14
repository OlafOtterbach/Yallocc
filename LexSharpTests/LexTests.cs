using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LexSharp
{
   [TestClass]
   public class LexTests
   {
      [TestMethod]
      public void IsCompleteTest_CompleteRegisteredTokens_IsComplite()
      {
         var lex = new LexSharp<AbcTokenType>();
         lex.Register(@"aabb", AbcTokenType.aabb_token);
         lex.Register(@"a(\w)+b", AbcTokenType.aXYZb_token);
         lex.Register("a", AbcTokenType.a_token);
         lex.Register("b", AbcTokenType.b_token);
         lex.Register("c", AbcTokenType.c_token);

         Assert.IsTrue(lex.IsComplete());
      }

      [TestMethod]
      public void RegisterTest_AllTokensOneTime_NoException()
      {
         bool exeptionThrown = false;
         try
         {
            var lex = new LexSharp<AbcTokenType>();
            lex.Register(@"aabb", AbcTokenType.aabb_token);
            lex.Register(@"a(\w)+b", AbcTokenType.aXYZb_token);
            lex.Register("a", AbcTokenType.a_token);
            lex.Register("b", AbcTokenType.b_token);
            lex.Register("c", AbcTokenType.c_token);
         }
         catch (TokenRegisteredMoreThanOneTimeException<AbcTokenType>)
         {
            exeptionThrown = true;
         }

         Assert.IsFalse(exeptionThrown);
      }

      [TestMethod]
      public void RegisterTest_OneTokenRegisteredTwice_Exception()
      {
         bool exeptionThrown = false;
         try
         {
            var lex = new LexSharp<AbcTokenType>();
            lex.Register(@"aabb", AbcTokenType.aabb_token);
            lex.Register(@"a(\w)+b", AbcTokenType.aXYZb_token);

            lex.Register("a", AbcTokenType.a_token);
            lex.Register("a", AbcTokenType.a_token);

            lex.Register("b", AbcTokenType.b_token);
            lex.Register("c", AbcTokenType.c_token);
         }
         catch (TokenRegisteredMoreThanOneTimeException<AbcTokenType> e)
         {
            exeptionThrown = true;
            Assert.AreEqual(e.Message, "Not allowed to register Token more than one time");
            Assert.AreEqual(e.TokenType, AbcTokenType.a_token);
         }

         Assert.IsTrue(exeptionThrown);
      }

      [TestMethod]
      public void IsCompleteTest_NotCompleteRegisteredTokens_IsNotComplite()
      {
         var lex = new LexSharp<AbcTokenType>();
         lex.Register(@"aabb", AbcTokenType.aabb_token);
         lex.Register(@"a(\w)+b", AbcTokenType.aXYZb_token);
         lex.Register("a", AbcTokenType.a_token);

         lex.Register("c", AbcTokenType.c_token);

         Assert.IsFalse(lex.IsComplete());
      }

      [TestMethod]
      public void ScanTest_EmptyToken_NoToken()
      {
         var lex = new LexSharp<AbcTokenType>();
         lex.Register("", AbcTokenType.a_token);
         var text = "aabbcc";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, text.Length + 1);
         Assert.IsTrue(tokens.All(x => x.Type.Equals(AbcTokenType.a_token)));
      }


      [TestMethod]
      public void ScanTest_NoText_NoToken()
      {
         var lex = CreateAbcLex();
         var text = "";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, 0);
      }


      [TestMethod]
      public void ScanTest_Empty_NoToken()
      {
         var lex = CreateAbcLex();
         var text = string.Empty;

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, 0);
      }


      [TestMethod]
      public void ScanTest_Xyz_NoToken()
      {
         var lex = CreateAbcLex();
         var text = @"xxxyyyzzzsssddfflltttrrrr";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, 0);
      }


      [TestMethod]
      public void ScanTest_TypicalAbcText_FirstPatternAabbFound()
      {
         var lex = CreateAbcLex();
         var text = @"aabbcc";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, 2);
         Assert.IsTrue(tokens[0].Type.Equals(AbcTokenType.aabb_token));
         Assert.IsTrue(tokens[1].Type.Equals(AbcTokenType.c_token));
         Assert.AreEqual(tokens[0].Value, "aabb");
         Assert.AreEqual(tokens[1].Value, "cc");
      }


      [TestMethod]
      public void ScanTest_TypicalAbcText_LongestPatternAxyzbFound()
      {
         var lex = CreateAbcLex();
         var text = @"aabbbbcc";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, 2);
         Assert.IsTrue(tokens[0].Type.Equals(AbcTokenType.aXYZb_token));
         Assert.IsTrue(tokens[1].Type.Equals(AbcTokenType.c_token));
         Assert.AreEqual(tokens[0].Value, "aabbbb");
         Assert.AreEqual(tokens[1].Value, "cc");
      }


      [TestMethod]
      public void ScanTest_AbcTextWithEndOfLine_CorrectPatternsFound()
      {
         var lex = CreateAbcLex();
         var text = "\naa\nbb\ncc\naabb\naccb\n";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, 5);
         Assert.IsTrue(tokens[0].Type.Equals(AbcTokenType.a_token));
         Assert.IsTrue(tokens[1].Type.Equals(AbcTokenType.b_token));
         Assert.IsTrue(tokens[2].Type.Equals(AbcTokenType.c_token));
         Assert.IsTrue(tokens[3].Type.Equals(AbcTokenType.aabb_token));
         Assert.IsTrue(tokens[4].Type.Equals(AbcTokenType.aXYZb_token));
         Assert.AreEqual(tokens[0].Value, "aa");
         Assert.AreEqual(tokens[1].Value, "bb");
         Assert.AreEqual(tokens[2].Value, "cc");
         Assert.AreEqual(tokens[3].Value, "aabb");
         Assert.AreEqual(tokens[4].Value, "accb");
      }


      [TestMethod]
      public void ScanTest_AbcTextWithSpaceSeparation_CorrectPatternsFound()
      {
         var lex = CreateAbcLex();
         var text = "aa bb cc accb aabb";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, 5);
         Assert.IsTrue(tokens[0].Type.Equals(AbcTokenType.a_token));
         Assert.IsTrue(tokens[1].Type.Equals(AbcTokenType.b_token));
         Assert.IsTrue(tokens[2].Type.Equals(AbcTokenType.c_token));
         Assert.IsTrue(tokens[3].Type.Equals(AbcTokenType.aXYZb_token));
         Assert.IsTrue(tokens[4].Type.Equals(AbcTokenType.aabb_token));
         Assert.AreEqual(tokens[0].Value, "aa");
         Assert.AreEqual(tokens[1].Value, "bb");
         Assert.AreEqual(tokens[2].Value, "cc");
         Assert.AreEqual(tokens[3].Value, "accb");
         Assert.AreEqual(tokens[4].Value, "aabb");
      }


      private LexSharp<AbcTokenType> CreateAbcLex()
      {
         var lex = new LexSharp<AbcTokenType>();
         lex.Register(@"(a)+", AbcTokenType.a_token);
         lex.Register(@"aabb", AbcTokenType.aabb_token);
         lex.Register(@"a(\w)+b", AbcTokenType.aXYZb_token);
         lex.Register(@"(b)+", AbcTokenType.b_token);
         lex.Register(@"(c)+", AbcTokenType.c_token);
         return lex;
      }
   }
}
