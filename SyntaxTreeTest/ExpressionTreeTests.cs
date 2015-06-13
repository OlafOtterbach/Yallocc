using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YallocSyntaxTree;

namespace YalloccSyntaxTreeTest
{
   [TestClass]
   public class ExpressionTreeTests
   {
      [TestMethod]
      public void Test_TwoPlusThree_Five()
      {
         var ctx = Parse("2+3");

         Assert.IsNotNull(ctx.Root);
         var res = ExpressionCalculator.Calculate(ctx.Root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 5);
      }

      [TestMethod]
      public void Test_TwoMultThree_Six()
      {
         var ctx = Parse("2*3");

         Assert.IsNotNull(ctx.Root);
         var res = ExpressionCalculator.Calculate(ctx.Root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 6);
      }

      [TestMethod]
      public void Test_TwoPlusThreeMultFour_Twenty()
      {
         var ctx = Parse("2+3*4");

         Assert.IsNotNull(ctx.Root);
         var res = ExpressionCalculator.Calculate(ctx.Root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 14);
      }

      [TestMethod]
      public void Test_TwoPlusThreePlusFour_Nine()
      {
         var ctx = Parse("2+3+4");

         Assert.IsNotNull(ctx.Root);
         var res = ExpressionCalculator.Calculate(ctx.Root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 9);
      }

      [TestMethod]
      public void Test_TwoNultThreeNultFour_TwentyFour()
      {
         var ctx = Parse("2*3*4");

         Assert.IsNotNull(ctx.Root);
         var res = ExpressionCalculator.Calculate(ctx.Root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 24);
      }

      [TestMethod]
      public void Test_ClampTwoPlusThreeClamp_Five()
      {
         var ctx = Parse("(2+3)");

         Assert.IsNotNull(ctx.Root);
         var res = ExpressionCalculator.Calculate(ctx.Root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 5);
      }

      [TestMethod]
      public void Test_TwoMultClampThreePlusFourClamp_Fourteen()
      {
         var ctx = Parse("2*(3+4)");

         Assert.IsNotNull(ctx.Root);
         var res = ExpressionCalculator.Calculate(ctx.Root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 14);
      }

      [TestMethod]
      public void Test_TwoPlusThreeMultFourMultClampFivePusSixClamp_OneHundreThirtyFour()
      {
         var ctx = Parse("2+3*4*(5+6)");

         Assert.IsNotNull(ctx.Root);
         var res = ExpressionCalculator.Calculate(ctx.Root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 134);
      }

      [TestMethod]
      public void Test_MinusTwoPlusThreeMultClampFourPlusClampFiveMultClampMinusSixClampClampClamp_MinusEighty()
      {
         var ctx = Parse("-2+3*(4+(5*(-6)))");

         Assert.IsNotNull(ctx.Root);
         var res = ExpressionCalculator.Calculate(ctx.Root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, -80);
      }

      private SyntaxTreeBuilder Parse(string text)
      {
         var generator = new ExpressionGrammarGenerator();
         var context = new SyntaxTreeBuilder();
         var parser = generator.CreateParser(context);
         var res = parser.Parse(text);
         context = res.Success ? context : null;
         return context;
      }
   }
}
