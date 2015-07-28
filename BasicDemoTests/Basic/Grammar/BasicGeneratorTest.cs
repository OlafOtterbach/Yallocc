using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicDemo;
using System.IO;
using BasicDemo.Basic;
using SyntaxTree;

namespace BasicDemoTest
{
   [TestClass]
   public class BasicGeneratorTest
   {
      [TestMethod]
      public void Test()
      {
         var programText = File.ReadAllText(@"Basic\Grammar\TestData\LetStatementProgram.basic");
         var stb = new SyntaxTreeBuilder();
         var generator = new BasicGrammarGenerator();
         var res = generator.Parse(programText);
         Assert.IsTrue(res.Success);
      }
   }
}
