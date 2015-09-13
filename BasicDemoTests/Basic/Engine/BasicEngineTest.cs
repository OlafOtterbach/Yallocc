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
