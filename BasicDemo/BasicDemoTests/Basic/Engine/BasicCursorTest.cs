using BasicDemo.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxTree;
using System.Linq;

namespace BasicDemoTest
{
   [TestClass]
   public class BasicCursorTest
   {
      public BasicEngine _engine;

      [TestMethod]
      public void NothingTest_Cursor_CursorsCurrenIsNull()
      {
         CreateProgram();
         var cursor = _engine.Cursor;

         Assert.IsNull(cursor.Current);
      }

      [TestMethod]
      public void ResetTest_Program_CursorOnFirstCommand()
      {
         CreateProgram();
         var cursor = _engine.Cursor;

         cursor.Reset();

         Assert.AreEqual(cursor.Current, _engine.Program.ToArray()[0]);
      }

      [TestMethod]
      public void NextGlobalTest_RunProgram_CursorOnEndOfProgram()
      {
         CreateProgram();
         var cursor = _engine.Cursor;
         cursor.Reset();
         _engine.Run();

         Assert.IsTrue(cursor.Current is EndOfProgramCommand);
         Assert.AreEqual(cursor.Current, _engine.Program.Last());
      }

      private void CreateProgram()
      {
         var text = "DIM a(2)\r\nLET i = 1\r\nLET i = i + 1\r\nLET a(1) = i";
         Create(text);
      }

      private void Create(string text)
      {
         _engine = CreateEngine();
         var synTree = CreateSyntaxTree(text);
         var programCreator = new ProgramCreator(_engine);
         programCreator.Create(synTree);
      }

      private SyntaxTreeNode CreateSyntaxTree(string text)
      {
         var programText = "PROGRAM \"ExpressionTest\"\r\n" + text + "\r\nEND";
         var grammarGenerator = new BasicGrammarGenerator();
         var res = grammarGenerator.Parse(programText);
         var node = res.Success ? res.Root : null;
         return node;
      }

      private BasicEngine CreateEngine()
      {
         var engine = new BasicEngine();
         return engine;
      }
   }
}
