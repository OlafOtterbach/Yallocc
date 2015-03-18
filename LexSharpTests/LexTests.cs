﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LexSharp
{
   [TestClass]
   public class LexTests
   {
      [TestMethod]
      public void ScanTest_EmptyToken_NoToken()
      {
         var lex = new Lex();
         lex.Register("", new AbcTokenType(AbcTokenTypes.a_token));
         var text = "aabbcc";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, text.Length + 1);
         Assert.IsTrue(tokens.All(x => x.Type.Equals(new AbcTokenType(AbcTokenTypes.a_token))));
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
         Assert.IsTrue(tokens[0].Type.Equals(new AbcTokenType(AbcTokenTypes.aabb_token)));
         Assert.IsTrue(tokens[1].Type.Equals(new AbcTokenType(AbcTokenTypes.c_token)));
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
         Assert.IsTrue(tokens[0].Type.Equals(new AbcTokenType(AbcTokenTypes.aXYZb_token)));
         Assert.IsTrue(tokens[1].Type.Equals(new AbcTokenType(AbcTokenTypes.c_token)));
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
         Assert.IsTrue(tokens[0].Type.Equals(new AbcTokenType(AbcTokenTypes.a_token)));
         Assert.IsTrue(tokens[1].Type.Equals(new AbcTokenType(AbcTokenTypes.b_token)));
         Assert.IsTrue(tokens[2].Type.Equals(new AbcTokenType(AbcTokenTypes.c_token)));
         Assert.IsTrue(tokens[3].Type.Equals(new AbcTokenType(AbcTokenTypes.aabb_token)));
         Assert.IsTrue(tokens[4].Type.Equals(new AbcTokenType(AbcTokenTypes.aXYZb_token)));
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
         Assert.IsTrue(tokens[0].Type.Equals(new AbcTokenType(AbcTokenTypes.a_token)));
         Assert.IsTrue(tokens[1].Type.Equals(new AbcTokenType(AbcTokenTypes.b_token)));
         Assert.IsTrue(tokens[2].Type.Equals(new AbcTokenType(AbcTokenTypes.c_token)));
         Assert.IsTrue(tokens[3].Type.Equals(new AbcTokenType(AbcTokenTypes.aXYZb_token)));
         Assert.IsTrue(tokens[4].Type.Equals(new AbcTokenType(AbcTokenTypes.aabb_token)));
         Assert.AreEqual(tokens[0].Value, "aa");
         Assert.AreEqual(tokens[1].Value, "bb");
         Assert.AreEqual(tokens[2].Value, "cc");
         Assert.AreEqual(tokens[3].Value, "accb");
         Assert.AreEqual(tokens[4].Value, "aabb");
      }


      private Lex CreateAbcLex()
      {
         var lex = new Lex();
         lex.Register(@"(a)+", new AbcTokenType(AbcTokenTypes.a_token));
         lex.Register(@"aabb", new AbcTokenType(AbcTokenTypes.aabb_token));
         lex.Register(@"a(\w)+b", new AbcTokenType(AbcTokenTypes.aXYZb_token));
         lex.Register(@"(b)+", new AbcTokenType(AbcTokenTypes.b_token));
         lex.Register(@"(c)+", new AbcTokenType(AbcTokenTypes.c_token));
         return lex;
      }
   }
}