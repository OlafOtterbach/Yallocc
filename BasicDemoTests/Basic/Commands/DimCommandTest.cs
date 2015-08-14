using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BasicDemo.Basic;
using LexSharp;

namespace BasicDemoTest
{
   [TestClass]
   public class DimCommandTest
   {
      [TestMethod]
      public void ExecuteTest_ArrayOfOneDimensionWithSizeThree_StoredInEngine()
      {
         var postorder = new List<BasicEntity> 
         { 
            new BasicInteger(3)
         };
         var expressionCmd = new ExpressionCommand(new Token<TokenType>("0.0", 0, 3), postorder);
         var engine = new BasicEngine();
         var dim = new DimCommand(new Token<TokenType>("a", 0, 1), engine, "a", expressionCmd);

         dim.Execute();

         var a = engine.GetVariable("a") as BasicArray;
         Assert.IsNotNull(a);
         Assert.AreEqual(1, a.Dimensions);
         Assert.AreEqual(3, a.DimensionSize(0));
      }

      [TestMethod]
      public void ExecuteTest_ArrayOfTwoDimensionWithSizeThree_StoredInEngine()
      {
         var postorder1 = new List<BasicEntity> 
         { 
            new BasicInteger(3)
         };
         var postorder2 = new List<BasicEntity> 
         { 
            new BasicInteger(4)
         };
         var expressionCmd1 = new ExpressionCommand(new Token<TokenType>("0.0", 0, 3), postorder1);
         var expressionCmd2 = new ExpressionCommand(new Token<TokenType>("0.0", 0, 3), postorder2);
         var engine = new BasicEngine();
         var dim = new DimCommand(new Token<TokenType>("a", 0, 1), engine, "a", expressionCmd1, expressionCmd2);

         dim.Execute();

         var a = engine.GetVariable("a") as BasicArray;
         Assert.IsNotNull(a);
         Assert.AreEqual(2, a.Dimensions);
         Assert.AreEqual(3, a.DimensionSize(0));
         Assert.AreEqual(4, a.DimensionSize(1));
      }

      [TestMethod]
      public void ExecuteTest_ArrayOfThreeDimensionWithSizeThree_StoredInEngine()
      {
         var postorder1 = new List<BasicEntity> 
         { 
            new BasicInteger(3)
         };
         var postorder2 = new List<BasicEntity> 
         { 
            new BasicInteger(4)
         };
         var postorder3 = new List<BasicEntity> 
         { 
            new BasicInteger(5)
         };
         var expressionCmd1 = new ExpressionCommand(new Token<TokenType>("0.0", 0, 3), postorder1);
         var expressionCmd2 = new ExpressionCommand(new Token<TokenType>("0.0", 0, 3), postorder2);
         var expressionCmd3 = new ExpressionCommand(new Token<TokenType>("0.0", 0, 3), postorder3);
         var engine = new BasicEngine();
         var dim = new DimCommand(new Token<TokenType>("a", 0, 1), engine, "a", expressionCmd1, expressionCmd2, expressionCmd3);

         dim.Execute();

         var a = engine.GetVariable("a") as BasicArray;
         Assert.IsNotNull(a);
         Assert.AreEqual(3, a.Dimensions);
         Assert.AreEqual(3, a.DimensionSize(0));
         Assert.AreEqual(4, a.DimensionSize(1));
         Assert.AreEqual(5, a.DimensionSize(2));
      }
   }
}