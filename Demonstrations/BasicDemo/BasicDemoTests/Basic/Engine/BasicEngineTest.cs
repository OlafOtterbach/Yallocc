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
      public void AssignTest_AddOneAddTwoAddThree_Six()
      {
         var program = "LET x = 1 + 2 + 3";
         var engine = Create(program);
         engine.Run();

         var x = (engine.Memory["x"] as BasicInteger).Value;
         Assert.AreEqual(6, x);
      }

      [TestMethod]
      public void AssignTest_AssignXYArrayElementValueToVariable_AssignedValue()
      {
         var program = "DIM a(2,4)\r\nLET x=0\r\nLET y=2\r\nLET a(x,y)=77\r\nLET i = a(x,y)";
         var engine = Create(program);
         engine.Run();

         var i = (engine.Memory["i"] as BasicInteger).Value;
         Assert.AreEqual(77, i);
      }

      [TestMethod]
      public void AssignTest_AssignTwoDimensionArrayElementValueToVariable_AssignedValue()
      {
         var program = "DIM a(3,3)\r\nLET a(2,2)=77\r\nLET i = a(2,2)";
         var engine = Create(program);
         engine.Run();

         var i = (engine.Memory["i"] as BasicInteger).Value;
         Assert.AreEqual(77, i);
      }

      [TestMethod]
      public void AssignTest_AssignArrayElementValueToVariable_AssignedValue()
      {
         var program = "DIM a(1)\r\nLET a(0)=77\r\nLET i = a(0)";
         var engine = Create(program);
         engine.Run();

         var i = (engine.Memory["i"] as BasicInteger).Value;
         Assert.AreEqual(77, i);
      }

      [TestMethod]
      public void AssignTest_AssignVariableToVariable_AssignedValue()
      {
         var program = "LET j = 77\r\nLET i = j";
         var engine = Create(program);
         engine.Run();

         var i = (engine.Memory["i"] as BasicInteger).Value;
         Assert.AreEqual(77, i);
      }

      [TestMethod]
      public void AssignTest_AssignValueToArrayElement_AssignedValue()
      {
         var program = "LET i = 77\r\nDIM a(1)\r\nLET a(0)=i";
         var engine = Create(program);
         engine.Run();

         var a = (engine.Memory["a"] as BasicArray);
         var val = (a.Get(0) as BasicInteger).Value;
         Assert.AreEqual(77, val);
      }

      [TestMethod]
      public void ForTest_TwoForLoops_Four()
      {
         var program = "LET a = 0\r\nFOR i=1 TO 2 DO\r\nFOR j=1 TO 2 DO\r\nLET a = a+1\r\nEND\r\nEND";
         var engine = Create(program);
         engine.Run();

         var a = (engine.Memory["a"] as BasicInteger).Value;
         Assert.AreEqual(4, a);
      }

      [TestMethod]
      public void ForTest_UntilTen_IndexIsTen()
      {
         var program = "FOR i=1 TO 10 DO\r\n  LET a = i\r\nEND";
         var engine = Create(program);
         engine.Run();

         var i = (engine.Memory["i"] as BasicInteger).Value;
         var a = (engine.Memory["a"] as BasicInteger).Value;
         Assert.AreEqual(10, i);
         Assert.AreEqual(10, a);
      }

      [TestMethod]
      public void ForTest_UntilTWo_IndexIsTwo()
      {
         var program = "FOR i=1 TO 2 DO\r\n  LET a = i * 2\r\nEND";
         var engine = Create(program);
         engine.Run();

         var i = (engine.Memory["i"] as BasicInteger).Value;
         var a = (engine.Memory["a"] as BasicInteger).Value;
         Assert.AreEqual(2, i);
         Assert.AreEqual(4, a);
      }

      [TestMethod]
      public void ForTest_UntilTenStepTwo_IndexIsTen()
      {
         var program = "LET a = 0\r\nFOR i=0 TO 10 STEP 1+1 DO\r\n  LET a = a + i\r\nEND";
         var engine = Create(program);
         engine.Run();

         var i = (engine.Memory["i"] as BasicInteger).Value;
         var a = (engine.Memory["a"] as BasicInteger).Value;
         Assert.AreEqual(10, i);
         Assert.AreEqual(2+4+6+8+10, a);
      }

      [TestMethod]
      public void WhileTest_UntilTen_IndexIsTen()
      {
         var program = "LET i=1\r\nWHILE i < 10 DO\r\n  LET i = i + 1\r\nEND";
         var engine = Create(program);
         engine.Run();

         var i = (engine.Memory["i"] as BasicInteger).Value;
         Assert.AreEqual(i, 10);
      }

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
