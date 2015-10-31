using BasicDemo.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxTree;
using System.Linq;

namespace BasicDemoTest
{
   [TestClass]
   public class ProgramCreatorTest
   {
      public BasicEngine _engine;

      [TestMethod]
      public void CreateTest_SimpleDefinitionProgram_DimAndLetCommands()
      {
         var text = "DIM a(2)\r\nLET i = 1\r\nLET i = i + 1\r\nLET a(1) = i";

         Create(text);

         Assert.AreEqual(5, _engine.Program.Count());
         Assert.AreEqual(1, _engine.Program.OfType<DimCommand>().Count());
         Assert.AreEqual(3, _engine.Program.OfType<LetCommand>().Count());
         Assert.AreEqual(1, _engine.Program.OfType<EndOfProgramCommand>().Count());
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