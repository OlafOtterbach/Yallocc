using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicDemo.Basic;
using SyntaxTree;
using System.Linq;

namespace BasicDemoTest.Basic
{
   [TestClass]
   public class BasicEngineTest
   {
      [TestMethod]
      public void IfThenEndTest_IfIsTrue_IfStatementExecuted()
      {
         var program = "LET a=1\r\nIF a < 2 THEN LET a = a + 2 END";
         var engine = Create(program);
         engine.Run();

         var a = (engine.Memory["a"] as BasicInteger).Value;
         Assert.AreEqual(a, 3);
      }

      [TestMethod]
      public void IfThenEndTest_IfIsFalse_IfStatementExecuted()
      {
         var program = "LET a=1\r\nIF a = 77 THEN LET a = a + 2 END";
         var engine = Create(program);
         engine.Run();

         var a = (engine.Memory["a"] as BasicInteger).Value;
         Assert.AreEqual(a, 1);
      }

      [TestMethod]
      public void IfThenElseEndTest_IfIsTrue_IfStatementExecutedElseStatementNotExecuted()
      {
         var program = "LET a=1\r\nIF a < 2 THEN LET a = 2 ELSE LET a = 3 END";
         var engine = Create(program);
         engine.Run();

         var a = (engine.Memory["a"] as BasicInteger).Value;
         Assert.AreEqual(a, 2);
      }

      [TestMethod]
      public void IfThenElseEndTest_IfIsFalse_IfStatementNotExecutedElseStatementExecuted()
      {
         var program = "LET a = 1\r\nLET b = 1\r\nIF a > 2 THEN\r\n  LET a = 2\r\n  LET b = 3\r\nELSE\r\n  LET a = 3\r\n  LET b = 4\r\nEND";
         var engine = Create(program);
         engine.Run();

         var a = (engine.Memory["a"] as BasicInteger).Value;
         var b = (engine.Memory["b"] as BasicInteger).Value;
         Assert.AreEqual(a, 3);
         Assert.AreEqual(b, 4);
      }

      [TestMethod]
      public void GotoTest()
      {
         var program = "LET a=1\r\nGOTO Hallo\r\nLET a = 2\r\nHallo:\r\nLET b = 3";
         var engine = Create(program);
         engine.Run();
         var a = (engine.Memory["a"] as BasicInteger).Value;
         var b = (engine.Memory["b"] as BasicInteger).Value;

         Assert.AreEqual(a, 1);
         Assert.AreEqual(b, 3);
      }

      private BasicEngine Create(string text)
      {
         var root = CreateSyntaxTree(text);
         var engine = new BasicEngine();
         var programCreator = new ProgramCreator(engine);
         programCreator.Create(root);
         return engine;
      }

      private SyntaxTreeNode CreateSyntaxTree(string text)
      {
         var programText = "PROGRAM \"ProgramTest\"\r\n" + text + "\r\nEND";
         var grammarGenerator = new BasicGrammarGenerator();
         var res = grammarGenerator.Parse(programText);
         return res.Root;
      }
   }
}
