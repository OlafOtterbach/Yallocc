using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LexSharp
{
   [TestClass]
   public class CodeGroupTest
   {
      [TestMethod]
      public void ScanTest_TokenOverlappingCodeGroup_TenTokens()
      {
         var lex = Create();
         var binaries = "101111110110010000110001111110";

         var sequence = lex.Scan(binaries).ToList();

         Assert.AreEqual(10, sequence.Count());
         Assert.AreEqual(5, sequence[0].Type);
         Assert.AreEqual(7, sequence[0].Type);
         Assert.AreEqual(6, sequence[0].Type);
         Assert.AreEqual(6, sequence[0].Type);
         Assert.AreEqual(2, sequence[0].Type);
         Assert.AreEqual(0, sequence[0].Type);
         Assert.AreEqual(6, sequence[0].Type);
         Assert.AreEqual(1, sequence[0].Type);
         Assert.AreEqual(7, sequence[0].Type);
         Assert.AreEqual(110, sequence[0].Type);
      }

      [TestMethod]
      public void ScanTest_RandomCodeGroup_NoFailedTokens()
      {
         var lex = Create();
         var rand = new Random();
         var binaries = Enumerable.Range(0, 10000)
                                 .Select(i => rand.Next(0, 2))
                                 .Select(x => x.ToString())
                                 .Aggregate((current, elem) => current = current + elem);

         var sequence = lex.Scan(binaries).ToList();

         Assert.IsTrue(sequence.All(x => x.Type != null));
      }

      private LexSharp<long> Create()
      {
         var lex = new LexSharp<long>();
         lex.Register(@"000", 0);
         lex.Register(@"001", 1);
         lex.Register(@"010", 2);
         lex.Register(@"011", 3);
         lex.Register(@"100", 4);
         lex.Register(@"101", 5);
         lex.Register(@"110", 6);
         lex.Register(@"111", 7);
         return lex;
      }
   }
}