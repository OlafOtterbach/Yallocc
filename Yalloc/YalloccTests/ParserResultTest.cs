using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc;

namespace YalloccTests
{
   [TestClass]
   public class ParserResultTest
   {
      [TestMethod]
      public void Constructor_Nothing_SuccessOthersAreFalse()
      {
         var res = new ParserResult();

         Assert.IsTrue(res.Success);
         Assert.IsFalse(res.SyntaxError);
         Assert.IsFalse(res.GrammarOfTextNotComplete);
         Assert.AreEqual(0,res.Position);
      }

      [TestMethod]
      public void SyntaxErrorTest_SyntaxErrorOnTrue_SyntaxErrorOthersAreFalse()
      {
         var res = new ParserResult();
         res.SyntaxError = true;

         Assert.IsFalse(res.Success);
         Assert.IsTrue(res.SyntaxError);
         Assert.IsFalse(res.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void SyntaxErrorTest_SyntaxErrorOnFalse_SuccessOthersAreFalse()
      {
         var res = new ParserResult();
         res.SyntaxError = false;

         Assert.IsTrue(res.Success);
         Assert.IsFalse(res.SyntaxError);
         Assert.IsFalse(res.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void GrammarOfTextNotCompleteTest_GrammarOfTextNotCompleteOnTrue_GrammarOfTextNotCompleteAreFalse()
      {
         var res = new ParserResult();
         res.GrammarOfTextNotComplete = true;

         Assert.IsFalse(res.Success);
         Assert.IsFalse(res.SyntaxError);
         Assert.IsTrue(res.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void GrammarOfTextNotComplete_GrammarOfTextNotCompleteOnFalse_GrammarOfTextNotCompleteAreFalse()
      {
         var res = new ParserResult();
         res.GrammarOfTextNotComplete = false;

         Assert.IsTrue(res.Success);
         Assert.IsFalse(res.SyntaxError);
         Assert.IsFalse(res.GrammarOfTextNotComplete);
      }
   }
}
