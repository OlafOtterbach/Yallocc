using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Yallocc.Tokenizer.LexSharp;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class LexSharpTokenizerTest : TokenizerTest
   {
      protected override TokenizerCreator<AbcTokenType> GetCreator()
      {
         return new LexSharpCreator<AbcTokenType>();
      }

      [TestMethod]
      public void ScanTest_EmptyToken_AllNonCharactersBetweenTextAndEnd()
      {
         var creator = GetCreator();
         creator.Register("", AbcTokenType.a_token);
         var tokenizer = creator.Create();
         var text = "aabbcc";

         var tokens = tokenizer.Scan(text).ToList();

         // Count = Emptyness before plus all between and after aabbcc plus invalid tokens a, a, b, b, c and c
         int expectedCount = 1 + text.Length - 1 + 1 + text.Length;
         Assert.AreEqual(expectedCount, tokens.Count);
         Assert.AreEqual("", tokens[0].Value);
         Assert.AreEqual("a", tokens[1].Value);
         Assert.AreEqual("", tokens[2].Value);
         Assert.AreEqual("a", tokens[3].Value);
         Assert.AreEqual("", tokens[4].Value);
         Assert.AreEqual("b", tokens[5].Value);
         Assert.AreEqual("", tokens[6].Value);
         Assert.AreEqual("b", tokens[7].Value);
         Assert.AreEqual("", tokens[8].Value);
         Assert.AreEqual("c", tokens[9].Value);
         Assert.AreEqual("", tokens[10].Value);
         Assert.AreEqual("c", tokens[11].Value);
         Assert.AreEqual("", tokens[12].Value);
      }
   }
}
