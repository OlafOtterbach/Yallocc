using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LexSharp
{
   [TestClass]
   public class LexTests
   {
      [TestMethod]
      public void TestMethod1()
      {
         var lex = CreateAbcLex();
         var text = @"aabbcc";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, 2);
         Assert.IsTrue(tokens[0].Type.Equals(new AbcTokenType(AbcTokenTypes.aabb_token)));
         Assert.IsTrue(tokens[1].Type.Equals(new AbcTokenType(AbcTokenTypes.c_token)));
      }

      [TestMethod]
      public void TestMethod2()
      {
         var lex = CreateAbcLex();
         var text = @"aabbbbcc";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, 2);
         Assert.IsTrue(tokens[0].Type.Equals(new AbcTokenType(AbcTokenTypes.aXYZb_token)));
         Assert.IsTrue(tokens[1].Type.Equals(new AbcTokenType(AbcTokenTypes.c_token)));
      }

      [TestMethod]
      public void TestMethod3()
      {
         var lex = CreateAbcLex();
         var text = "aa\nbb\ncc\naabb\naccb\n";

         var tokens = lex.Scan(text).ToList();

         Assert.AreEqual(tokens.Count, 5);
         Assert.IsTrue(tokens[0].Type.Equals(new AbcTokenType(AbcTokenTypes.a_token)));
         Assert.IsTrue(tokens[1].Type.Equals(new AbcTokenType(AbcTokenTypes.c_token)));
         Assert.IsTrue(tokens[2].Type.Equals(new AbcTokenType(AbcTokenTypes.c_token)));
         Assert.IsTrue(tokens[3].Type.Equals(new AbcTokenType(AbcTokenTypes.aabb_token)));
         Assert.IsTrue(tokens[4].Type.Equals(new AbcTokenType(AbcTokenTypes.aXYZb_token)));
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
