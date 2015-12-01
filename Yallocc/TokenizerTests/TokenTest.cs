using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class TokenTest
   {
      [TestMethod]
      public void ConstructorTest_WhenOneParameter_ThenTypeIsGivenOthersDefault()
      {
         var token = new Token<AbcTokenType>(AbcTokenType.a_token);

         Assert.AreEqual(AbcTokenType.a_token, token.Type);
         Assert.AreEqual(0, token.TextIndex);
         Assert.AreEqual(0, token.Length);
         Assert.AreEqual(string.Empty, token.Value);
         Assert.IsTrue(token.IsValid);
      }


      [TestMethod]
      public void ConstructorTest_WhenNoTypeIsGiven_ThenTypeIsNullValuesAreSet()
      {
         var token = new Token<AbcTokenType>("Hugo", 1, 2);

         Assert.AreEqual(null, token.Type);
         Assert.AreEqual(1, token.TextIndex);
         Assert.AreEqual(2, token.Length);
         Assert.AreEqual("Hugo", token.Value);
      }


      [TestMethod]
      public void ConstructorTest_WhenAllParameter_ThenValuesAreSet()
      {
         var token = new Token<AbcTokenType>("Hugo", AbcTokenType.a_token, 1, 2);

         Assert.AreEqual(AbcTokenType.a_token, token.Type);
         Assert.AreEqual(1, token.TextIndex);
         Assert.AreEqual(2, token.Length);
         Assert.AreEqual("Hugo", token.Value);
         Assert.IsTrue(token.IsValid);
      }


      [TestMethod]
      public void IsValidTest_WhenTypeIsNull_ThenTokenNotValid()
      { 
         var token = new Token<AbcTokenType>(string.Empty, 0, 0);

         Assert.AreEqual(null, token.Type);
         Assert.IsFalse(token.IsValid);
      }
   }
}