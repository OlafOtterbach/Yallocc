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
         Assert.AreEqual(5, res.DoubleValue);
      }

      [TestMethod]
      public void Test_FiveEqualsTwoPlusThree_True()
      {
         var root = Parse("5=2+3");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsFalse(res.IsDouble);
         Assert.IsTrue(res.IsBoolean);
         Assert.AreEqual(true, res.BooleanValue);
      }

      [TestMethod]
      public void Test_BooleanExpressionEqualsBooleanExpression_True()
      {
         var root = Parse("((5=2+3)=(2+2=4))");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsFalse(res.IsDouble);
         Assert.IsTrue(res.IsBoolean);
         Assert.AreEqual(true, res.BooleanValue);
      }

      [TestMethod]
      public void Test_TwoMultThree_Six()
      {
         var root = Parse("2*3");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(6, res.DoubleValue);
      }

      [TestMethod]
      public void Test_TwoPlusThreeMultFour_Twenty()
      {
         var root = Parse("2+3*4");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(14, res.DoubleValue);
      }

      [TestMethod]
      public void Test_TwoPlusThreePlusFour_Nine()
      {
         var root = Parse("2+3+4");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(9, res.DoubleValue);
      }

      [TestMethod]
      public void Test_TwoNultThreeNultFour_TwentyFour()
      {
         var root = Parse("2*3*4");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(24, res.DoubleValue);
      }

      [TestMethod]
      public void Test_ClampTwoPlusThreeClamp_Five()
      {
         var root = Parse("(2+3)");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(5, res.DoubleValue);
      }

      [TestMethod]
      public void Test_TwoMultClampThreePlusFourClamp_Fourteen()
      {
         var root = Parse("2*(3+4)");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(14, res.DoubleValue);
      }

      [TestMethod]
      public void Test_TwoPlusThreeMultFourMultClampFivePusSixClamp_OneHundreThirtyFour()
      {
         var root = Parse("2+3*4*(5+6)");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(134, res.DoubleValue);
      }

      [TestMethod]
      public void Test_MinusTwoPlusThreeMultClampFourPlusClampFiveMultClampMinusSixClampClampClamp_MinusEighty()
      {
         var root = Parse("-2+3*(4+(5*(-6)))");

         Assert.IsNotNull(root);
         var res = ExpressionCalculator.Calculate(root);
         Assert.IsTrue(res.IsDouble);
         Assert.AreEqual(-80, res.DoubleValue);
      }

      private SyntaxTreeNode Parse(string text)
      {
         var grammarGenerator = new ExpressionGrammarGenerator();
         var treeGenerator = grammarGenerator.CreateParser();
         var res = treeGenerator.Parse(text);
         var node = res.Success ? res.Root : null;
         return node;
      }
   }
}