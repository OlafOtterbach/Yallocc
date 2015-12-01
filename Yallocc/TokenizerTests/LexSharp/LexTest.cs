using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class LexTest
   {
      [TestMethod]
      public void IsCompleteTest_CompleteRegisteredTokens_IsComplete()
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
      public void IsCompleteTest_NotAnEnumType_TokenIsNotAnEnumTypeExceptionThrown()
      {
         var lex = new LexSharp<int>();
         lex.Register(@"one", 1);
         lex.Register(@"Two", 2);
         lex.Register(@"Three", 3);

         bool exeptionThrown = false;
         try
         {
            bool isComplete = lex.IsComplete();
         }
         catch (TokenIsNotAnEnumTypeException e)
         {
            exeptionThrown = true;
         }
         Assert.IsTrue(exeptionThrown);
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
            Assert.AreEqual("Not allowed to register Token more than one time", e.Message);
            Assert.AreEqual(AbcTokenType.a_token, e.TokenType);
         }

         Assert.IsTrue(exeptionThrown);
      }

      [TestMethod]
      public void ScanTest_EmptyToken_AllNonCharactersBetweenTextAndEnd()
      {
         var lex = new LexSharp<AbcTokenType>();
         lex.Register("", AbcTokenType.a_token);
         var text = "aabbcc";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(text.Length, tokens.Count);
         Assert.IsTrue(tokens.All(x => x.Type.Equals(AbcTokenType.a_token)));
      }


      [TestMethod]
      public void ScanTest_NoText_NoToken()
      {
         var lex = CreateAbcLex();
         var text = "";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(0, tokens.Count);
      }


      [TestMethod]
      public void ScanTest_Empty_NoToken()
      {
         var lex = CreateAbcLex();
         var text = string.Empty;

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(0, tokens.Count);
      }


      [TestMethod]
      public void ScanTest_Xyz_NoValidToken()
      {
         var lex = CreateAbcLex();
         var text = @"xxxyyyzzzsssddfflltttrrrr";

         var tokens = lex.Scan(text).ToList();

         Assert.IsTrue(tokens.All(tok => !tok.IsValid));
         Assert.AreEqual(1, tokens.Count);
      }


      [TestMethod]
      public void ScanTest_TypicalAbcText_FirstPatternIsLongestAabb()
      {
         var lex = CreateAbcLex();
         var text = @"aabbcc";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(2, tokens.Count);
         Assert.IsTrue(tokens[0].Type.Equals(AbcTokenType.aabb_token));
         Assert.IsTrue(tokens[1].Type.Equals(AbcTokenType.c_token));
         Assert.AreEqual("aabb", tokens[0].Value);
         Assert.AreEqual("cc", tokens[1].Value);
      }


      [TestMethod]
      public void ScanTest_TypicalAbcText_LongestPatternAxyzbFound()
      {
         var lex = CreateAbcLex();
         var text = @"aabbbbcc";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(2, tokens.Count);
         Assert.IsTrue(tokens[0].Type.Equals(AbcTokenType.aXYZb_token));
         Assert.IsTrue(tokens[1].Type.Equals(AbcTokenType.c_token));
         Assert.AreEqual("aabbbb", tokens[0].Value);
         Assert.AreEqual("cc", tokens[1].Value);
      }


      [TestMethod]
      public void ScanTest_AbcTextWithEndOfLine_CorrectPatternsFound()
      {
         var lex = CreateAbcLex();
         var text = "\naa\nbb\ncc\naabb\naccb\n";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(11, tokens.Count);
         Assert.IsFalse(tokens[0].IsValid);
         Assert.IsTrue(tokens[1].IsValid);
         Assert.IsTrue(tokens[1].Type.Equals(AbcTokenType.a_token));
         Assert.IsFalse(tokens[2].IsValid);
         Assert.IsTrue(tokens[3].IsValid);
         Assert.IsTrue(tokens[3].Type.Equals(AbcTokenType.b_token));
         Assert.IsFalse(tokens[4].IsValid);
         Assert.IsTrue(tokens[5].IsValid);
         Assert.IsTrue(tokens[5].Type.Equals(AbcTokenType.c_token));
         Assert.IsFalse(tokens[6].IsValid);
         Assert.IsTrue(tokens[7].IsValid);
         Assert.IsTrue(tokens[7].Type.Equals(AbcTokenType.aabb_token));
         Assert.IsFalse(tokens[8].IsValid);
         Assert.IsTrue(tokens[9].IsValid);
         Assert.IsTrue(tokens[9].Type.Equals(AbcTokenType.aXYZb_token));
         Assert.IsFalse(tokens[10].IsValid);
         Assert.AreEqual("aa", tokens[1].Value);
         Assert.AreEqual("bb", tokens[3].Value);
         Assert.AreEqual("cc", tokens[5].Value);
         Assert.AreEqual("aabb", tokens[7].Value);
         Assert.AreEqual("accb", tokens[9].Value);
      }


      [TestMethod]
      public void ScanTest_AbcTextWithSpaceSeparation_CorrectPatternsFound()
      {
         var lex = CreateAbcLex();
         var text = "aa bb cc accb aabb";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(9, tokens.Count);
         Assert.IsTrue(tokens[0].IsValid);
         Assert.IsTrue(tokens[0].Type.Equals(AbcTokenType.a_token));
         Assert.IsFalse(tokens[1].IsValid);
         Assert.IsTrue(tokens[2].IsValid);
         Assert.IsTrue(tokens[2].Type.Equals(AbcTokenType.b_token));
         Assert.IsFalse(tokens[3].IsValid);
         Assert.IsTrue(tokens[4].IsValid);
         Assert.IsTrue(tokens[4].Type.Equals(AbcTokenType.c_token));
         Assert.IsFalse(tokens[5].IsValid);
         Assert.IsTrue(tokens[6].IsValid);
         Assert.IsTrue(tokens[6].Type.Equals(AbcTokenType.aXYZb_token));
         Assert.IsFalse(tokens[7].IsValid);
         Assert.IsTrue(tokens[8].IsValid);
         Assert.IsTrue(tokens[8].Type.Equals(AbcTokenType.aabb_token));
         Assert.IsFalse(tokens[1].IsValid);
         Assert.AreEqual("aa", tokens[0].Value);
         Assert.AreEqual("bb", tokens[2].Value);
         Assert.AreEqual("cc", tokens[4].Value);
         Assert.AreEqual("accb", tokens[6].Value);
         Assert.AreEqual("aabb", tokens[8].Value);
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
