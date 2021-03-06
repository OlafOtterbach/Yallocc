﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public abstract class TokenizerTest
   {
      protected abstract TokenizerCreator<AbcTokenType> GetCreator();

      [TestMethod]
      public void ScanTest_HugoToken_NonValidHugoScanned()
      {
         var tokenizer = CreateAbctokenizer();
         var text = "Hugo";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(1, tokens.Count);
         Assert.AreEqual("Hugo", tokens[0].Value);
      }

      [TestMethod]
      public void ScanTest_ASemicolonB_NonValidSemicolonScanned()
      {
         var tokenizer = CreateAbctokenizer();
         var text = "a;b";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(3, tokens.Count);
         Assert.AreEqual(";", tokens[1].Value);
      }

      [TestMethod]
      public void ScanTest_ASpaceB_NonValidSpaceScanned()
      {
         var tokenizer = CreateAbctokenizer();
         var text = "a b";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(3, tokens.Count);
         Assert.AreEqual(" ", tokens[1].Value);
      }

      [TestMethod]
      public void ScanTest_NoText_NoToken()
      {
         var tokenizer = CreateAbctokenizer();
         var text = "";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(0, tokens.Count);
      }


      [TestMethod]
      public void ScanTest_Empty_NoToken()
      {
         var tokenizer = CreateAbctokenizer();
         var text = string.Empty;

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(0, tokens.Count);
      }


      [TestMethod]
      public void ScanTest_Xyz_NoValidToken()
      {
         var tokenizer = CreateAbctokenizer();
         var text = @"xxxyyyzzzsssddfflltttrrrr";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.IsTrue(tokens.All(tok => !tok.IsValid));
         Assert.AreEqual(1, tokens.Count);
      }

      [TestMethod]
      public void ScanTest_aaxxx_NoValidToken()
      {
         var tokenizer = CreateAbctokenizer();
         var text = @"aaxxx";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(2, tokens.Count);
         Assert.AreEqual("aa", tokens[0].Value);
         Assert.AreEqual("xxx", tokens[1].Value);
      }

      [TestMethod]
      public void ScanTest_xxxaa_NoValidToken()
      {
         var tokenizer = CreateAbctokenizer();
         var text = @"xxxaa";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(2, tokens.Count);
         Assert.AreEqual("xxx", tokens[0].Value);
         Assert.AreEqual("aa", tokens[1].Value);
      }

      [TestMethod]
      public void ScanTest_TypicalAbcText_FirstPatternIsLongestAabb()
      {
         var tokenizer = CreateAbctokenizer();
         var text = @"aabbcc";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(2, tokens.Count);
         Assert.IsTrue(tokens[0].Type.Equals(AbcTokenType.aabb_token));
         Assert.IsTrue(tokens[1].Type.Equals(AbcTokenType.c_token));
         Assert.AreEqual("aabb", tokens[0].Value);
         Assert.AreEqual("cc", tokens[1].Value);
      }

      [TestMethod]
      public void ScanTest_TypicalAbcText_LongestPatternAxyzbFound()
      {
         var tokenizer = CreateAbctokenizer();
         var text = @"aabbbbcc";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(3, tokens.Count);
         Assert.IsTrue(tokens[0].Type.Equals(AbcTokenType.aabb_token));
         Assert.IsTrue(tokens[1].Type.Equals(AbcTokenType.b_token));
         Assert.IsTrue(tokens[2].Type.Equals(AbcTokenType.c_token));
         Assert.AreEqual("aabb", tokens[0].Value);
         Assert.AreEqual("bb", tokens[1].Value);
         Assert.AreEqual("cc", tokens[2].Value);
      }

      [TestMethod]
      public void ScanTest_AbcTextWithSpaceSeparation_CorrectPatternsFound()
      {
         var tokenizer = CreateAbctokenizer();
         var text = "aa bb cc accb aabb";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(11, tokens.Count);
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
         Assert.IsTrue(tokens[6].Type.Equals(AbcTokenType.a_token));
         Assert.IsTrue(tokens[7].IsValid);
         Assert.IsTrue(tokens[7].Type.Equals(AbcTokenType.c_token));
         Assert.IsTrue(tokens[8].IsValid);
         Assert.IsTrue(tokens[8].Type.Equals(AbcTokenType.b_token));
         Assert.IsFalse(tokens[9].IsValid);
         Assert.IsTrue(tokens[10].IsValid);
         Assert.IsTrue(tokens[10].Type.Equals(AbcTokenType.aabb_token));
         Assert.AreEqual("aa", tokens[0].Value);
         Assert.AreEqual("bb", tokens[2].Value);
         Assert.AreEqual("cc", tokens[4].Value);
         Assert.AreEqual("a", tokens[6].Value);
         Assert.AreEqual("cc", tokens[7].Value);
         Assert.AreEqual("b", tokens[8].Value);
         Assert.AreEqual("aabb", tokens[10].Value);
      }

      [TestMethod]
      public void ScanTest_AbcTextWithEndOfLine_CorrectPatternsFound()
      {
         var tokenizer = CreateAbctokenizer();
         var text = "\naa\nbb\ncc\naabb\naccb\n";

         var tokens = tokenizer.Scan(text).ToList();

         Assert.AreEqual(13, tokens.Count);
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
         Assert.IsTrue(tokens[9].Type.Equals(AbcTokenType.a_token));
         Assert.IsTrue(tokens[10].IsValid);
         Assert.IsTrue(tokens[10].Type.Equals(AbcTokenType.c_token));
         Assert.IsTrue(tokens[11].IsValid);
         Assert.IsTrue(tokens[11].Type.Equals(AbcTokenType.b_token));
         Assert.IsFalse(tokens[12].IsValid);
         Assert.AreEqual("aa", tokens[1].Value);
         Assert.AreEqual("bb", tokens[3].Value);
         Assert.AreEqual("cc", tokens[5].Value);
         Assert.AreEqual("aabb", tokens[7].Value);
         Assert.AreEqual("a", tokens[9].Value);
         Assert.AreEqual("cc", tokens[10].Value);
         Assert.AreEqual("b", tokens[11].Value);
      }


      protected Tokenizer<AbcTokenType> CreateAbctokenizer()
      {
         var creator = GetCreator();
         creator.Register(@"a(d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z)+b", AbcTokenType.aXYZb_token);
         creator.Register(@"aabb", AbcTokenType.aabb_token);
         creator.Register(@"(a)+", AbcTokenType.a_token);
         creator.Register(@"(b)+", AbcTokenType.b_token);
         creator.Register(@"(c)+", AbcTokenType.c_token);
         var tokenizer = creator.Create();
         return tokenizer;
      }
   }
}
