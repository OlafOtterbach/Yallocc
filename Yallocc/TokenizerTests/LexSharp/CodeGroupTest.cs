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
         Assert.AreEqual(7, sequence[1].Type);
         Assert.AreEqual(6, sequence[2].Type);
         Assert.AreEqual(6, sequence[3].Type);
         Assert.AreEqual(2, sequence[4].Type);
         Assert.AreEqual(0, sequence[5].Type);
         Assert.AreEqual(6, sequence[6].Type);
         Assert.AreEqual(1, sequence[7].Type);
         Assert.AreEqual(7, sequence[8].Type);
         Assert.AreEqual(6, sequence[9].Type);
      }

      [TestMethod]
      public void ScanTest_RandomCodeGroupWithTrippelGroups_NoFailedTokens()
      {
         const int elementsLimit = 10000;
         const int limit = elementsLimit * 3;
         var lex = Create();
         var rand = new Random();
         var binaries = Enumerable.Range(0, limit)
                                 .Select(i => rand.Next(0, 2))
                                 .Select(x => x.ToString())
                                 .Aggregate((current, elem) => current = current + elem);

         var sequence = lex.Scan(binaries).ToList();

         Assert.IsTrue(sequence.All(x => x.Type != null));
      }

      [TestMethod]
      public void ScanTest_RandomCodeGroupLengthModuloThreeIsOne_OneFailedTokens()
      {
         const int elementsLimit = 10000;
         const int limit = elementsLimit * 3 + 1;
         var lex = Create();
         var rand = new Random();
         var binaries = Enumerable.Range(0, limit)
                                 .Select(i => rand.Next(0, 2))
                                 .Select(x => x.ToString())
                                 .Aggregate((current, elem) => current = current + elem);

         var sequence = lex.Scan(binaries).ToList();

         Assert.AreEqual(1, sequence.Count(x => x.Type == null));
      }

      [TestMethod]
      public void ScanTest_RandomCodeGroupLengthModuloThreeIsTwo_OneFailedTokens()
      {
         const int elementsLimit = 10000;
         const int limit = elementsLimit * 3 + 2;
         var lex = Create();
         var rand = new Random();
         var binaries = Enumerable.Range(0, limit)
                                 .Select(i => rand.Next(0, 2))
                                 .Select(x => x.ToString())
                                 .Aggregate((current, elem) => current = current + elem);

         var sequence = lex.Scan(binaries).ToList();

         Assert.AreEqual(1, sequence.Count(x => x.Type == null));
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