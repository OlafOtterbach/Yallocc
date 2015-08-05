using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BasicDemo.Basic;
using LexSharp;

namespace BasicDemoTest
{
   [TestClass]
   public class ExpressionCommandTest
   {
      [TestMethod]
      public void ExecuteTest_TwoAddThree_Five()
      {
         Assert.IsTrue(BinaryOperatorExpressionTest(2, new BasicAddition(), 3, 5));
      }

      [TestMethod]
      public void ExecuteTest_TwoMinusThree_MinusOne()
      {
         Assert.IsTrue(BinaryOperatorExpressionTest(2, new BasicSubtraction(), 3, -1));
      }

      [TestMethod]
      public void ExecuteTest_TwoMultThree_Six()
      {
         Assert.IsTrue(BinaryOperatorExpressionTest(2, new BasicMultiplication(), 3, 6));
      }

      [TestMethod]
      public void ExecuteTest_SixDivThree_Two()
      {
         Assert.IsTrue(BinaryOperatorExpressionTest(6, new BasicDivision(), 3, 2));
      }

      [TestMethod]
      public void ExecuteTest_ThreeEqualsThree_True()
      {
         Assert.IsTrue(BinaryOperatorCompareTest(3, new BasicEquals(), 3, true));
      }

      [TestMethod]
      public void ExecuteTest_TwoEqualsThree_False()
      {
         Assert.IsTrue(BinaryOperatorCompareTest(2, new BasicEquals(), 3, false));
      }

      [TestMethod]
      public void ExecuteTest_StringEqualsString_True()
      {
         var postorder = new List<BasicEntity> 
         { 
            new BasicString("Hallo"),
            new BasicString("Hallo"),
            new BasicEquals(),
         };
         var expressionCmd = new ExpressionCommand(new Token<TokenType>("Hallo", 0, 5), postorder);

         var res = expressionCmd.Execute();

         Assert.IsTrue(res.IsBoolean);
         Assert.AreEqual(true, (res as BasicBoolean).Value);
      }

      [TestMethod]
      public void ExecuteTest_TwoLessThree_True()
      {
         Assert.IsTrue(BinaryOperatorCompareTest(2, new BasicLess(), 3, true));
      }

      [TestMethod]
      public void ExecuteTest_ThreeLessThree_False()
      {
         Assert.IsTrue(BinaryOperatorCompareTest(3, new BasicLess(), 3, false));
      }

      [TestMethod]
      public void ExecuteTest_ThreeGreaterTwo_True()
      {
         Assert.IsTrue(BinaryOperatorCompareTest(3, new BasicGreater(), 2, true));
      }

      [TestMethod]
      public void ExecuteTest_TwoGreaterThree_False()
      {
         Assert.IsTrue(BinaryOperatorCompareTest(2, new BasicGreater(), 3, false));
      }

      [TestMethod]
      public void ExecuteTest_NegateTwo_False()
      {
         var postorder = new List<BasicEntity> 
         { 
            new BasicReal(2.0),
            new BasicNegation(),
         };
         var expressionCmd = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorder);

         var res = expressionCmd.Execute();

         Assert.IsTrue(res.IsReal);
         var floatRes = res as BasicReal;
         Assert.AreEqual(-2.0, floatRes.Value);
      }

      [TestMethod]
      public void ExecuteTest_PlusTwo_False()
      {
         var postorder = new List<BasicEntity> 
         { 
            new BasicReal(2.0),
            new BasicAdditionSign(),
         };
         var expressionCmd = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorder);

         var res = expressionCmd.Execute();

         Assert.IsTrue(res.IsReal);
         var floatRes = res as BasicReal;
         Assert.AreEqual(2.0, floatRes.Value);
      }

      [TestMethod]
      public void ExecuteTest_AddTwoStrings_ConcatedString()
      {
         var postorder = new List<BasicEntity> 
         {
            new BasicString("Hallo"),
            new BasicString("Halli"),
            new BasicAddition(),
         };
         var expressionCmd = new ExpressionCommand(new Token<TokenType>("Hallo", 0, 5), postorder);

         var res = expressionCmd.Execute();

         Assert.IsTrue(res.IsString);
         Assert.AreEqual("HalliHallo", (res as BasicString).Value);
      }

      [TestMethod]
      public void ExecuteTest_TwoMultThreePlusOne_Seven()
      {
         var postorder = new List<BasicEntity> 
         { 
            new BasicReal(2.0),
            new BasicReal(3.0),
            new BasicMultiplication(),
            new BasicReal(1.0), 
            new BasicAddition()
         };
         var expressionCmd = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorder);

         var res = expressionCmd.Execute();

         Assert.IsTrue(res.IsReal);
         var floatRes = res as BasicReal;
         Assert.AreEqual(7.0, floatRes.Value);
      }

      [TestMethod]
      public void ExecuteTest_TwoMultThreePlusOneLessEight_True()
      {
         var postorder = new List<BasicEntity> 
         { 
            new BasicReal(8.0),
            new BasicReal(2.0),
            new BasicReal(3.0),
            new BasicMultiplication(),
            new BasicReal(1.0), 
            new BasicAddition(),
            new BasicLess()
         };
         var expressionCmd = new ExpressionCommand(new Token<TokenType>("8.0", 0, 3), postorder);

         var res = expressionCmd.Execute();

         Assert.IsTrue(res.IsBoolean);
         var boolRes = res as BasicBoolean;
         Assert.IsTrue(boolRes.Value);
      }

      [TestMethod]
      public void ExecuteTest_TwoMultThreePlusOneLessSeven_False()
      {
         var postorder = new List<BasicEntity> 
         { 
            new BasicReal(7.0),
            new BasicReal(2.0),
            new BasicReal(3.0),
            new BasicMultiplication(),
            new BasicReal(1.0), 
            new BasicAddition(),
            new BasicLess()
         };
         var expressionCmd = new ExpressionCommand(new Token<TokenType>("7.0", 0, 3), postorder);

         var res = expressionCmd.Execute();

         Assert.IsTrue(res.IsBoolean);
         var boolRes = res as BasicBoolean;
         Assert.IsFalse(boolRes.Value);
      }

      private bool BinaryOperatorExpressionTest(int left, BasicBinaryOperator op, int right, int result)
      {
         var resInt = result;
         var resFloat = (double)result;
         var leftInteger = new BasicInteger(left);
         var leftFloat = new BasicReal((double)left);
         var rightInteger = new BasicInteger(right);
         var rightFloat = new BasicReal((double)right);

         var postorderIntInt = new List<BasicEntity> { rightInteger, leftInteger, op };
         var postorderIntFloat = new List<BasicEntity> { rightFloat, leftInteger, op };
         var postorderFloatInt = new List<BasicEntity> { rightInteger, leftFloat, op };
         var postorderFloatFloat = new List<BasicEntity> { rightFloat, leftFloat, op };
         var resIntInt = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorderIntInt).Execute();
         var resIntFloat = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorderIntFloat).Execute();
         var resFloatInt = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorderFloatInt).Execute();
         var resFloatFloat = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorderFloatFloat).Execute();

         Assert.IsTrue(resIntInt.IsInteger);
         Assert.IsTrue(resFloatInt.IsReal);
         Assert.IsTrue(resIntFloat.IsReal);
         Assert.IsTrue(resFloatFloat.IsReal);
         Assert.AreEqual(resInt, (resIntInt as BasicInteger).Value);
         Assert.AreEqual(resFloat, (resFloatInt as BasicReal).Value);
         Assert.AreEqual(resFloat, (resIntFloat as BasicReal).Value);
         Assert.AreEqual(resFloat, (resFloatFloat as BasicReal).Value);
         return true;
      }

      private bool BinaryOperatorCompareTest(int left, BasicBinaryOperator op, int right, bool result)
      {
         var leftInteger = new BasicInteger(left);
         var leftFloat = new BasicReal((double)left);
         var rightInteger = new BasicInteger(right);
         var rightFloat = new BasicReal((double)right);

         var postorderIntInt = new List<BasicEntity> { rightInteger, leftInteger, op };
         var postorderIntFloat = new List<BasicEntity> { rightFloat, leftInteger, op };
         var postorderFloatInt = new List<BasicEntity> { rightInteger, leftFloat, op };
         var postorderFloatFloat = new List<BasicEntity> { rightFloat, leftFloat, op };
         var resIntInt = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorderIntInt).Execute();
         var resIntFloat = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorderIntFloat).Execute();
         var resFloatInt = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorderFloatInt).Execute();
         var resFloatFloat = new ExpressionCommand(new Token<TokenType>("2.0", 0, 3), postorderFloatFloat).Execute();

         Assert.IsTrue(resIntInt.IsBoolean);
         Assert.IsTrue(resFloatInt.IsBoolean);
         Assert.IsTrue(resIntFloat.IsBoolean);
         Assert.IsTrue(resFloatFloat.IsBoolean);
         Assert.AreEqual(result, (resIntInt as BasicBoolean).Value);
         Assert.AreEqual(result, (resFloatInt as BasicBoolean).Value);
         Assert.AreEqual(result, (resIntFloat as BasicBoolean).Value);
         Assert.AreEqual(result, (resFloatFloat as BasicBoolean).Value);
         return true;
      }


      private BasicEntity ExecuteBinaryOperatorExpression(BasicEntity left, BasicBinaryOperator op, BasicEntity right)
      {
         var postorder = new List<BasicEntity> { left, right, op };
         return new ExpressionCommand(new Token<TokenType>("left", 0, 4), postorder).Execute();
      }
   }
}
