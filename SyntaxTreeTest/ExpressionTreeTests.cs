using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxTree;
using SyntaxTreeTest.ExpressionTree;

namespace SyntaxTreeTest
{
   [TestClass]
   public class ExpressionTreeTests
   {
      [TestMethod]
      public void Test_TwoPlusThree_Five()
      {
         var root = Parse("2+3");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 5);
      }

      [TestMethod]
      public void Test_FiveEqualsTwoPlusThree_True()
      {
         var root = Parse("5=2+3");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsFalse(res.IsDouble);
         Assert.IsTrue(res.IsBoolean);
         Assert.AreEqual(res.BooleanValue, true);
      }

      [TestMethod]
      public void Test_BooleanExpressionEqualsBooleanExpression_True()
      {
         var root = Parse("((5=2+3)=(2+2=4))");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsFalse(res.IsDouble);
         Assert.IsTrue(res.IsBoolean);
         Assert.AreEqual(res.BooleanValue, true);
      }

      [TestMethod]
      public void Test_TwoMultThree_Six()
      {
         var root = Parse("2*3");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 6);
      }

      [TestMethod]
      public void Test_TwoPlusThreeMultFour_Twenty()
      {
         var root = Parse("2+3*4");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 14);
      }

      [TestMethod]
      public void Test_TwoPlusThreePlusFour_Nine()
      {
         var root = Parse("2+3+4");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 9);
      }

      [TestMethod]
      public void Test_TwoNultThreeNultFour_TwentyFour()
      {
         var root = Parse("2*3*4");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 24);
      }

      [TestMethod]
      public void Test_ClampTwoPlusThreeClamp_Five()
      {
         var root = Parse("(2+3)");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 5);
      }

      [TestMethod]
      public void Test_TwoMultClampThreePlusFourClamp_Fourteen()
      {
         var root = Parse("2*(3+4)");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 14);
      }

      [TestMethod]
      public void Test_TwoPlusThreeMultFourMultClampFivePusSixClamp_OneHundreThirtyFour()
      {
         var root = Parse("2+3*4*(5+6)");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, 134);
      }

      [TestMethod]
      public void Test_MinusTwoPlusThreeMultClampFourPlusClampFiveMultClampMinusSixClampClampClamp_MinusEighty()
      {
         var root = Parse("-2+3*(4+(5*(-6)))");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(res.DoubleValue, -80);
      }

      private SyntaxTreeNode Parse(string text)
      {
         var generator = new ExpressionGrammarGenerator();
         var parser = generator.CreateParser();
         var res = parser.Parse(text);
         var node = res.Success ? res.Root : null;
         return node;
      }
   }
}
