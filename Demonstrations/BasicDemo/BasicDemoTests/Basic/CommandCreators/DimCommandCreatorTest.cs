using BasicDemo.Basic;
using Yallocc.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.SyntaxTree;
using System.Linq;

namespace BasicDemoTest
{
   [TestClass]
   public class DimCommandCreatorTest
   {
      public BasicEngine _engine;

      [TestMethod]
      public void ExecuteTest_DimOfNotExistingAOfTwoThreeFour_ArrayCreated()
      {
         CreateAndExecute("DIM a(2,3,4)");
         Validate("a", 2, 3, 4);
      }

      [TestMethod]
      public void ExecuteTest_DimOfExistingMatrixOfTwoThreeFour_BasicVariableAlreadyDefinedException()
      {
         bool exceptionRised = false;
         try
         {
            CreateAndExecute("DIM matrix(2,3,4)");
            Validate("matrix", 2, 3, 4);
         }
         catch(BasicVariableAlreadyDefinedException e)
         {
            exceptionRised = true;
         }

         Assert.IsTrue(exceptionRised);
      }

      private void CreateAndExecute(string text)
      {
         var command = Create(text);
         command.Execute();
      }

      private void Validate(string variable, params int[] expected)
      {
         var array = _engine.GetVariable(variable) as BasicArray;

         Assert.IsNotNull(array);
         Assert.AreEqual(expected.Length, array.Dimensions);
         Assert.IsTrue(expected.Select((x, i) => new { Size = x, Index = i }).All(elem => array.DimensionSize(elem.Index) == elem.Size));
      }

      private BasicCommand Create(string text)
      {
         _engine = CreateEngine();
         var synTree = CreateSyntaxTree(text);
         var letCreator = new DimCommandCreator(_engine);
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
         CreateMatrix(engine);

         return engine;
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
   }
}
