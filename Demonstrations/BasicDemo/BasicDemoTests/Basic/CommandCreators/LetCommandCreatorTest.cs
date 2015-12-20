using BasicDemo.Basic;
using Yallocc.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxTree;
using System.Collections.Generic;
using System.Linq;

namespace BasicDemoTest
{
   [TestClass]
   public class LetCommandCreatorTest
   {
      public BasicEngine _engine;

      [TestMethod]
      public void ExecuteTest_ExistingI3AndExpression_TwentyThree()
      {
         CreateAndExecute("LET i1 = 3 + 4 * 5");
         ValidateInteger("i1", 23);
      }

      [TestMethod]
      public void ExecuteTest_NewVariableAAndExpression_CreatedAisTwentyThree()
      {
         CreateAndExecute("LET a = 3 + 4 * 5");
         ValidateInteger("a", 23);
      }

      [TestMethod]
      public void ExecuteTest_LetNotExistingFieldOfThreeAndExpression_Exception()
      {
         CreateAndExecute("LET foo(3) = 3.0 + 4.0 * 5.0");
// ToDo:Exception, ValidateRealField("foo", 23.0);
      }

      [TestMethod]
      public void ExecuteTest_LetFiledOfThreeAndExpression_TwentyThree()
      {
         CreateAndExecute("LET field(3) = 3.0 + 4.0 * 5.0");
         ValidateRealField("field", 23.0);
      }

      [TestMethod]
      public void ExecuteTest_LetFiledOfThreeAndExpression_Twenty()
      {
         CreateAndExecute("LET field(3) = 20");
         ValidateRealField("field", 20.0);
      }

      [TestMethod]
      public void ExecuteTest_LetMatrixOfThreeOfTwoAndExpression_TwentyThree()
      {
         CreateAndExecute("LET matrix(3,2) = 3 + 4 * 5");
         ValidateMatrix(23);
      }

      [TestMethod]
      public void ExecuteTest_LetMatrixOfThreeOfTwoAndExpression_Twenty()
      {
         CreateAndExecute("LET matrix(3,2) = 20");
         ValidateMatrix(20);
      }

      private void CreateAndExecute(string text)
      {
         var command = Create(text);
         command.Execute();
      }

      private void ValidateInteger(string variable, int expected)
      {
         var i = _engine.GetVariable(variable) as BasicInteger;
         Assert.AreEqual(expected, i.Value);
      }

      private void ValidateRealField(string field, double expected)
      {
         var array = (_engine.GetVariable(field) as BasicArray);
         var access = new BasicArrayElementAccessor(array);
         var postorder = new List<BasicEntity> { new BasicInteger(3) };
         var expressionCommand = new ExpressionCommand(new Token<TokenType>("x", 0, 1), postorder);
         access.Add(expressionCommand);
         var realValue = (access.Value as BasicReal);
         Assert.AreEqual(expected, realValue.Value);
      }

      private void ValidateMatrix(int expected)
      {
         var array = (_engine.GetVariable("matrix") as BasicArray);
         var access = new BasicArrayElementAccessor(array);
         var postorder1 = new List<BasicEntity> { new BasicInteger(3) };
         var postorder2 = new List<BasicEntity> { new BasicInteger(2) };
         var expressionCommand1 = new ExpressionCommand(new Token<TokenType>("x", 0, 1), postorder1);
         var expressionCommand2 = new ExpressionCommand(new Token<TokenType>("x", 0, 1), postorder2);
         access.Add(expressionCommand1);
         access.Add(expressionCommand2);
         var intValue = (access.Value as BasicInteger);
         Assert.AreEqual(expected, intValue.Value);
      }

      private BasicCommand Create(string text)
      {
         _engine = CreateEngine();
         var synTree = CreateSyntaxTree(text);
         var letCreator = new LetCommandCreator(_engine);
         letCreator.Create(synTree);
         var letCommand = _engine.Program.Last();
         _engine.Add(new EndOfProgramCommand(new Token<TokenType>("end", 0, 3), _engine));
         _engine.Reset();
         return letCommand;
      }

      private SyntaxTreeNode CreateSyntaxTree(string text)
      {
         var programText = "PROGRAM \"ExpressionTest\"\r\n" + text + "\r\nEND";
         var grammarGenerator = new BasicGrammarGenerator();
         var res = grammarGenerator.Parse(programText);
         var node = res.Success ? res.Root.Children.ToArray()[1] : null;
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
         for (int i = 0; i < 10; i++)
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
