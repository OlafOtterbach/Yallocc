using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public abstract class TokenizerCreatorTest
   {
      private class TestCreator<T> : TokenizerCreator<T>
      {
         public override Tokenizer<T> Create()
         {
            return null;
         }
      }

      protected abstract TokenizerCreator<TokenType> GetCreator();

      [TestMethod]
      public void RegisterTest_EmptyPattern_ArgumentException()
      {
         var creator = GetCreator();
         var exeptionThrown = false;
         var exceptionMessage = string.Empty;
         try
         {
            creator.Register("", AbcTokenType.a_token);
         }
         catch (ArgumentException e)
         {
            exeptionThrown = true;
            exceptionMessage = e.Message;
         }
         Assert.IsTrue(exeptionThrown);
         Assert.AreEqual("Empty patterntext is not a valid regular expression for the tokenizer.", exceptionMessage);
      }

      [TestMethod]
      public void IsCompleteTest_WhenCompleteRegisteredTokens_ThenCreatorIsComplete()
      {
         var creator = new TestCreator<AbcTokenType>();
         creator.Register(@"aabb", AbcTokenType.aabb_token);
         creator.Register(@"a(\w)+b", AbcTokenType.aXYZb_token);
         creator.Register("a", AbcTokenType.a_token);
         creator.Register("b", AbcTokenType.b_token);
         creator.Register("c", AbcTokenType.c_token);

         creator.IsTrue(tokenizer.IsComplete());
      }

      [TestMethod]
      public void IsCompleteTest_WhenNotCompleteRegisteredTokens_ThenCreatorIsNotComplite()
      {
         var creator = new TestCreator<AbcTokenType>();
         creator.Register(@"aabb", AbcTokenType.aabb_token);
         creator.Register(@"a(\w)+b", AbcTokenType.aXYZb_token);
         creator.Register("a", AbcTokenType.a_token);
         creator.Register("c", AbcTokenType.c_token);

         Assert.IsFalse(tokenizer.IsComplete());
      }

      [TestMethod]
      public void IsCompleteTest_WhenRegistersNotAnEnumType_ThenTokenIsNotAnEnumTypeExceptionThrown()
      {
         var creator = new TestCreator<AbcTokenType>();
         creator.Register(@"Two", 2);
         creator.Register(@"Three", 3);
         bool exeptionThrown = false;
         try
         {
            bool isComplete = tokenizer.IsComplete();
         }
         catch (TokenIsNotAnEnumTypeException e)
         {
            exeptionThrown = true;
         }
         Assert.IsTrue(exeptionThrown);
      }

      [TestMethod]
      public void RegisterTest_WhenRegisterAllTokensOneTime_ThenNoException()
      {
         bool exeptionThrown = false;
         try
         {
            var creator = new TestCreator<AbcTokenType>();
            creator.Register(@"aabb", AbcTokenType.aabb_token);
            creator.Register(@"a(\w)+b", AbcTokenType.aXYZb_token);
            creator.Register("a", AbcTokenType.a_token);
            creator.Register("b", AbcTokenType.b_token);
            creator.Register("c", AbcTokenType.c_token);
         }
         catch (TokenRegisteredMoreThanOneTimeException<AbcTokenType>)
         {
            exeptionThrown = true;
         }

         Assert.IsFalse(exeptionThrown);
      }

      [TestMethod]
      public void RegisterTest_WhenRegisterOneTokenRegisteredTwice_ThenException()
      {
         bool exeptionThrown = false;
         try
         {
            var creator = new TestCreator<AbcTokenType>();
            creator.Register(@"aabb", AbcTokenType.aabb_token);
            creator.Register(@"a(\w)+b", AbcTokenType.aXYZb_token);
            creator.Register("a", AbcTokenType.a_token);
            creator.Register("a", AbcTokenType.a_token);
            creator.Register("b", AbcTokenType.b_token);
            creator.Register("c", AbcTokenType.c_token);
         }
         catch (TokenRegisteredMoreThanOneTimeException<AbcTokenType> e)
         {
            exeptionThrown = true;
            Assert.AreEqual("Not allowed to register Token more than one time", e.Message);
            Assert.AreEqual(AbcTokenType.a_token, e.TokenType);
         }

         Assert.IsTrue(exeptionThrown);
      }
   }
}
