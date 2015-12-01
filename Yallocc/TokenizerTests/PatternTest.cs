using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class PatternTest
   {
      [TestMethod]
      public void ConstructorTest_WhenCalledWithParameters_ThenParamtersAreSet()
      {
         var regex = new Regex("a");

         var pattern = new Pattern<AbcTokenType>(regex, AbcTokenType.a_token);

         Assert.AreEqual(regex, pattern.TokenPattern);
         Assert.AreEqual(AbcTokenType.a_token, pattern.TokenType);
      }
   }
}