using BasicDemo.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxTree;
using System.Linq;

namespace BasicDemoTest
{
   [TestClass]
   public class ExpressionCommandCreatorTest
   {
      [TestMethod]
      public void CreateTest_TwoPlusThree_ExpressionCommandForTwoPlusThree()
      {
         var expression = Create("2+3");
         ValidateInteger(expression, 5);
      }

      [TestMethod]
      public void CreateTest_MinusTwo_ExpressionCommandForMinus()
      {
         var expression = Create("-2");
         ValidateInteger(expression, -2);
      }

      [TestMethod]
      public void CreateTest_MultiIntegerTest_NoAssertion()
      {
         var expression = Create("2-3");
         ValidateInteger(expression, -1);

         expression = Create("2*3");
         ValidateInteger(expression, 6);

         expression = Create("6/2");
         ValidateInteger(expression, 3);

         expression = Create("2*(3+4)");
         ValidateInteger(expression, 14);

         expression = Create("(2 * (3 + 4)) / 2");
         ValidateInteger(expression, 7);
      }

      [TestMethod]
      public void CreateTest_MultiFloatTest_NoAssertion()
      {
         var expression = Create("2.0-3.0");
         ValidateFloat(expression, -1.0);

         expression = Create("2.0*3.0");
         ValidateFloat(expression, 6.0);

         expression = Create("6.0/2.0");
         ValidateFloat(expression, 3.0);

         expression = Create("2.0*(3.0+4.0)");
         ValidateFloat(expression, 14.0);

         expression = Create("(2.0 * (3.0 + 4.0)) / 2.0");
         ValidateFloat(expression, 7.0);

         expression = Create("2.0 * (2.0 + (-3.0))");
         ValidateFloat(expression, -2.0);
      }


      [TestMethod]
      public void CreateTest_MixedIntegerFloatTest_NoAssertion()
      {
         var expression = Create("2.0-3");
         ValidateFloat(expression, -1.0);

         expression = Create("2*3.0");
         ValidateFloat(expression, 6.0);

         expression = Create("6.0/2");
         ValidateFloat(expression, 3.0);

         expression = Create("2.0*(3.0+4)");
         ValidateFloat(expression, 14.0);

         expression = Create("(2 * (3 + 4)) / 2.0");
         ValidateFloat(expression, 7.0);

         expression = Create("2 * (2 + (-3.0))");
         ValidateFloat(expression, -2.0);
      }

      [TestMethod]
      public void CreateTest_MultiBooleanTestForInteger_NoAssertion()
      {
         var expression = Create("2=2");
         ValidateBoolean(expression, true);

         expression = Create("2=3");
         ValidateBoolean(expression, false);

         expression = Create("2*3=3+3");
         ValidateBoolean(expression, true);

         expression = Create("2<3");
         ValidateBoolean(expression, true);

         expression = Create("3 < 2");
         ValidateBoolean(expression, false);

         expression = Create("3 < 3");
         ValidateBoolean(expression, false);

         expression = Create("3 > 2");
         ValidateBoolean(expression, true);

         expression = Create("2 > 3");
         ValidateBoolean(expression, false);

         expression = Create("3 > 3");
         ValidateBoolean(expression, false);
      }

      [TestMethod]
      public void CreateTest_MultiBooleanTestForFloat_NoAssertion()
      {
         var expression = Create("2.0=2.0");
         ValidateBoolean(expression, true);

         expression = Create("2.0=3.0");
         ValidateBoolean(expression, false);

         expression = Create("2.0*3.0=3+3.0");
         ValidateBoolean(expression, true);

         expression = Create("2.0<3.0");
         ValidateBoolean(expression, true);

         expression = Create("3.0 < 2.0");
         ValidateBoolean(expression, false);

         expression = Create("3.0 < 3.0");
         ValidateBoolean(expression, false);

         expression = Create("3.0 > 2.0");
         ValidateBoolean(expression, true);

         expression = Create("2.0 > 3.0");
         ValidateBoolean(expression, false);

         expression = Create("3.0 > 3.0");
         ValidateBoolean(expression, false);
      }

      [TestMethod]
      public void CreateTest_ConstantTwoPlusVariableThree_Five()
      {
         var expression = Create("2 + three");
         ValidateInteger(expression, 5);
      }

      [TestMethod]
      public void CreateTest_OneDimensionalField_NoAssertions()
      {
         var expression = Create("field(0)");
         ValidateArrayAccessor(expression, 1.0);

         expression = Create("field(2 - 1)");
         ValidateArrayAccessor(expression, 2.0);

         expression = Create("2.0 * field(9)");
         ValidateFloat(expression, 20.0);

         expression = Create("2 * field(9)");
         ValidateFloat(expression, 20.0);

         expression = Create("field(2 * (1+2))");
         ValidateArrayAccessor(expression, 7.0);
      }

      public void ValidateInteger(ExpressionCommand expression, int expectedValue)
      {
         Assert.IsNotNull(expression);
         Assert.IsTrue(expression.Execute().IsInteger);
         var actualValue = (expression.Execute() as BasicInteger).Value;
         Assert.AreEqual(expectedValue, actualValue);
      }

      public void ValidateFloat(ExpressionCommand expression, double expectedValue)
      {
         Assert.IsNotNull(expression);
         Assert.IsTrue(expression.Execute().IsFloat);
         var actualValue = (expression.Execute() as BasicFloat).Value;
         Assert.AreEqual(expectedValue, actualValue);
      }

      public void ValidateArrayAccessor(ExpressionCommand expression, double expectedValue)
      {
         Assert.IsNotNull(expression);
         Assert.IsTrue(expression.Execute().IsArray);
         var actualValue = ((expression.Execute() as BasicArrayElementAccessor).Value as BasicFloat).Value;
         Assert.AreEqual(expectedValue, actualValue);
      }

      public void ValidateBoolean(ExpressionCommand expression, bool expectedValue)
      {
         Assert.IsNotNull(expression);
         Assert.IsTrue(expression.Execute().IsBoolean);
         var actualValue = (expression.Execute() as BasicBoolean).Value;
         Assert.AreEqual(expectedValue, actualValue);
      }

      private ExpressionCommand Create(string text)
      {
         var engine = CreateEngine();
         var synTree = CreateSyntaxTree(text);
         var expressionCreator = new ExpressionCommandCreator(engine);
         var expression = expressionCreator.Create(synTree);
         return expression;
      }
      
      private SyntaxTreeNode CreateSyntaxTree(string text)
      {
         var programText = "PROGRAM \"ExpressionTest\"\r\nLET nothing=" + text;
         var grammarGenerator = new BasicGrammarGenerator();
         var res = grammarGenerator.Parse(programText);
         var node = res.Success ? res.Root.Children.ToArray()[1].Children.ToArray()[2] : null;
         return node;
      }

      private BasicEngine CreateEngine()
      {
         var engine = new BasicEngine();

         engine.RegisterVariable("three", new BasicInteger(3));
         
         var field = new BasicArray(10);
         field.Set(new BasicFloat(1.0), 0);
         field.Set(new BasicFloat(2.0), 1);
         field.Set(new BasicFloat(3.0), 2);
         field.Set(new BasicFloat(4.0), 3);
         field.Set(new BasicFloat(5.0), 4);
         field.Set(new BasicFloat(6.0), 5);
         field.Set(new BasicFloat(7.0), 6);
         field.Set(new BasicFloat(8.0), 7);
         field.Set(new BasicFloat(9.0), 8);
         field.Set(new BasicFloat(10.0), 9);
         engine.RegisterVariable("field", field);

         return engine;
      }
   }
}