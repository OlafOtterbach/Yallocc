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
         ValidateRealArrayAccessor(expression, 1.0);

         expression = Create("field(2 - 1)");
         ValidateRealArrayAccessor(expression, 2.0);

         expression = Create("2.0 * field(9)");
         ValidateFloat(expression, 20.0);

         expression = Create("2 * field(9)");
         ValidateFloat(expression, 20.0);

         expression = Create("field(2 * (1+2))");
         ValidateRealArrayAccessor(expression, 7.0);
      }

      [TestMethod]
      public void CreateTest_TwoDimensionalFieldTwoConstantParameters_NoAssertions()
      {
         var expression = Create("matrix(4,7)");
         ValidateIntegerArrayAccessor(expression, 48);
      }

      [TestMethod]
      public void CreateTest_TwoDimensionalFieldTwoExpressions_NoAssertions()
      {
         var expression = Create("matrix(i3 * (2+i1),4 * i2 / 2)");
         ValidateIntegerArrayAccessor(expression, 95);
      }

      [TestMethod]
      public void CreateTest_TwoDimensionalField_NoAssertions()
      {
         var expression = Create("matrix(matrix(4 - 5 + 1, 1) * 4 - 1, matrix(2 - 2, 2) - 1)");
         ValidateIntegerArrayAccessor(expression, 73);

         expression = Create("matrix( matrix(0, 1), matrix(1, 0) /10 )");
         ValidateIntegerArrayAccessor(expression, 22);
      }

      [TestMethod]
      public void CreateTest_ThreeDimensionalField_NoAssertions()
      {
         var expression = Create("cube(cube(0,0,1),cube(0,1,0) / 10, cube(1,0,0) / 100)");
         ValidateIntegerArrayAccessor(expression, 101);
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
         Assert.IsTrue(expression.Execute().IsReal);
         var actualValue = (expression.Execute() as BasicReal).Value;
         Assert.AreEqual(expectedValue, actualValue);
      }

      public void ValidateRealArrayAccessor(ExpressionCommand expression, double expectedValue)
      {
         Assert.IsNotNull(expression);
         Assert.IsTrue(expression.Execute().IsArray);
         var actualValue = ((expression.Execute() as BasicArrayElementAccessor).Value as BasicReal).Value;
         Assert.AreEqual(expectedValue, actualValue);
      }

      public void ValidateIntegerArrayAccessor(ExpressionCommand expression, double expectedValue)
      {
         Assert.IsNotNull(expression);
         Assert.IsTrue(expression.Execute().IsArray);
         var actualValue = ((expression.Execute() as BasicArrayElementAccessor).Value as BasicInteger).Value;
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
         engine.RegisterVariable("i1", new BasicInteger(1));
         engine.RegisterVariable("i2", new BasicInteger(2));
         engine.RegisterVariable("i3", new BasicInteger(3));
         CreateField(engine);
         CreateMatrix(engine);
         CreateCube(engine);

         return engine;
      }

      private void CreateField(BasicEngine engine)
      {
         var field = new BasicArray(10);
         field.Set(new BasicReal(1.0), 0);
         field.Set(new BasicReal(2.0), 1);
         field.Set(new BasicReal(3.0), 2);
         field.Set(new BasicReal(4.0), 3);
         field.Set(new BasicReal(5.0), 4);
         field.Set(new BasicReal(6.0), 5);
         field.Set(new BasicReal(7.0), 6);
         field.Set(new BasicReal(8.0), 7);
         field.Set(new BasicReal(9.0), 8);
         field.Set(new BasicReal(10.0), 9);
         engine.RegisterVariable("field", field);
      }

      private void CreateMatrix(BasicEngine engine)
      {
         var matrix = new BasicArray(10, 10);
         var index = 1;
         for (int i = 0; i < 10; i++ )
         {
            for (int j = 0; j < 10; j++)
            {
               matrix.Set(new BasicInteger(index++), i, j);
            }
         }
         engine.RegisterVariable("matrix", matrix);
      }

      private void CreateCube(BasicEngine engine)
      {
         const int max = 5;
         var cube = new BasicArray(max, max, max);
         var index = 1;
         for (int i = 0; i < max; i++)
         {
            for (int j = 0; j < max; j++)
            {
               for (int k = 0; k < max; k++)
               {
                  cube.Set(new BasicInteger(index++), i, j, k);
               }
            }
         }
         engine.RegisterVariable("cube", cube);
      }
   }
}