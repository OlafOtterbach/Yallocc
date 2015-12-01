using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public abstract class CodeGroupTest
   {
      protected abstract TokenizerCreator<long> GetCreator();

      [TestMethod]
      public void ScanTest_TokenOverlappingCodeGroup_TenTokens()
      {
         var tokenizer = Create();
         var binaries = "101111110110010000110001111110";

         var sequence = tokenizer.Scan(binaries).ToList();

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
         var tokenizer = Create();
         var rand = new Random();
         var binaries = Enumerable.Range(0, limit)
                                 .Select(i => rand.Next(0, 2))
                                 .Select(x => x.ToString())
                                 .Aggregate((current, elem) => current = current + elem);

         var sequence = tokenizer.Scan(binaries).ToList();

         Assert.IsTrue(sequence.All(x => x.Type != null));
      }

      [TestMethod]
      public void ScanTest_RandomCodeGroupLengthModuloThreeIsOne_OneFailedTokens()
      {
         const int elementsLimit = 10000;
         const int limit = elementsLimit * 3 + 1;
         var tokenizer = Create();
         var rand = new Random();
         var binaries = Enumerable.Range(0, limit)
                                 .Select(i => rand.Next(0, 2))
                                 .Select(x => x.ToString())
                                 .Aggregate((current, elem) => current = current + elem);

         var sequence = tokenizer.Scan(binaries).ToList();

         Assert.AreEqual(1, sequence.Count(x => x.Type == null));
      }

      [TestMethod]
      public void ScanTest_RandomCodeGroupLengthModuloThreeIsTwo_OneFailedTokens()
      {
         const int elementsLimit = 10000;
         const int limit = elementsLimit * 3 + 2;
         var tokenizer = Create();
         var rand = new Random();
         var binaries = Enumerable.Range(0, limit)
                                 .Select(i => rand.Next(0, 2))
                                 .Select(x => x.ToString())
                                 .Aggregate((current, elem) => current = current + elem);

         var sequence = tokenizer.Scan(binaries).ToList();

         Assert.AreEqual(1, sequence.Count(x => x.Type == null));
      }

      private Tokenizer<long> Create()
      {
         var creator = GetCreator();
         creator.Register(@"000", 0);
         creator.Register(@"001", 1);
         creator.Register(@"010", 2);
         creator.Register(@"011", 3);
         creator.Register(@"100", 4);
         creator.Register(@"101", 5);
         creator.Register(@"110", 6);
         creator.Register(@"111", 7);
         return creator.Create();
      }
   }
}