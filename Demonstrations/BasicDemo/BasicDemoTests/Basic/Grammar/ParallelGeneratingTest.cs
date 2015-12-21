using BasicDemo.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Yallocc.SyntaxTree;

namespace BasicDemoTest
{
   [TestClass]
   public class ParallelGeneratingTest
   {
      [TestMethod]
      public void ParallelParseTest()
      {
         var programText = GenerateBasicProgram();
         var generator = new BasicGrammarGenerator();
         var results = Enumerable.Range(0, 4).AsParallel().Select(i => generator.Parse(programText)).Where(res => res.Success).Select(res => Retrieve(res.Root).ToList()).ToList();
         Assert.AreEqual(4, results.Count);
         Assert.IsTrue(results.Skip(1).All(x => x.Count() == results.First().Count()));
         Assert.IsTrue(results[0].Zip(results[1], (x, y) => AreEqual(x, y)).All(x => x));
         Assert.IsTrue(results[1].Zip(results[2], (x, y) => AreEqual(x, y)).All(x => x));
         Assert.IsTrue(results[2].Zip(results[3], (x, y) => AreEqual(x, y)).All(x => x));
      }


      private static bool AreEqual(SyntaxTreeNode first, SyntaxTreeNode second)
      {
         bool areEqual = first.Children.Count() == second.Children.Count() && first.GetType() == second.GetType();
         if(areEqual)
         {
            if(first is TokenTreeNode<TokenType>)
            {
               var tokFirst = ((TokenTreeNode<TokenType>)first).Token;
               var tokSecond = ((TokenTreeNode<TokenType>)second).Token;
               areEqual = tokFirst.Value == tokSecond.Value && tokFirst.Type == tokSecond.Type;
            }
         }
         return areEqual;
      }

      private static IEnumerable<SyntaxTreeNode> Retrieve(SyntaxTreeNode root)
      {
         yield return root;
         foreach (var item in root.Children.SelectMany(Retrieve))
         {
            yield return item;
         }
      }

      private string GenerateBasicProgram()
      {
         var program = "PROGRAM \"generated\"\r\n";
         program = GenerateSequence(program);
         program += "END";
         return program;
      }

      private string GenerateSequence(string program)
      {
         var rand = new Random();
         var count = 1000;
         for (int i = 0; i < count; i++)
         {
            var stmnt = rand.Next(1, 4);
            switch (stmnt)
            {
               case 1:
                  program = GenerateLetStatement(program);
                  break;
               case 2:
                  program = GenerateIfStatement(program);
                  break;
               case 3:
                  program = GenerateForStatement(program);
                  break;
               default:
                  break;
            }
         }
         return program;
      }

       private string GenerateLetStatement(string program)
      {
         var rand = new Random();
         var num = rand.Next(0, 26);
         var character = (char)((int)('a') + num);
         program += string.Format("LET {0}=2\r\n", character);
         program += string.Format("LET {0}={0} + 1\r\n", character);
         return program;
      }

      private string GenerateIfStatement(string program)
      {
         program += "LET x=2\r\n";
         program += "IF x > 2 THEN\r\n";
         program = GenerateLetStatement(program);
         program += "ELSE\r\n";
         program = GenerateLetStatement(program);
         program += "END\r\n";
         return program;
      }

      private string GenerateForStatement(string program)
      {
         program += "FOR i = 0 TO 10 DO\r\n";
         program = GenerateLetStatement(program);
         program += "END\r\n";
         return program;
      }
   }
}
