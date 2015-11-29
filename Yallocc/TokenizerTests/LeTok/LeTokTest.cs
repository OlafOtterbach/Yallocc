using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LexSharp
{
   [TestClass]
   public class LeTokTest
   {
      [TestMethod]
      public void ScanTest_WhenNull_ThenEmptySequence()
      {
         var lex = Create();
         string empty = null;

         var sequence = lex.Scan(empty).ToList();

         Assert.AreEqual(0, sequence.Count);
      }

      [TestMethod]
      public void ScanTest_WhenEmptyInput_ThenEmptySequence()
      {
         var lex = Create();
         var empty = "";

         var sequence = lex.Scan(empty).ToList();

         Assert.AreEqual(0, sequence.Count);
      }

      [TestMethod]
      public void ScanTest_WhenTokenOverlappingCodeGroup_ThenTenTokens()
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
      public void ScanTest_WhenTextHasInvalidTokens_ThenOneInvalidToken()
      {
         var lex = Create();
         var binaries = "Hugo";

         var sequence = lex.Scan(binaries).ToList();

         Assert.AreEqual(1, sequence.Count);
         Assert.AreEqual(1, sequence.Count(tok => !tok.IsValid));
      }

      [TestMethod]
      public void ScanTest_WhenFirstTokenInvalidAndTwoValidTokens_ThenOneInvalidTwoValid()
      {
         var lex = Create();
         var binaries = "Hugo110111";

         var sequence = lex.Scan(binaries).ToList();

         Assert.AreEqual(3, sequence.Count);
         Assert.IsFalse(sequence.First().IsValid);
         Assert.IsTrue(sequence.Skip(1).All(x => x.IsValid));
      }

      [TestMethod]
      public void ScanTest_WhenLastTokenInvalidAndTwoValidTokens_ThenOneInvalidTwoValid()
      {
         var lex = Create();
         var binaries = "110111Hugo";

         var sequence = lex.Scan(binaries).ToList();

         Assert.AreEqual(3, sequence.Count);
         Assert.IsFalse(sequence.Last().IsValid);
         Assert.IsTrue(sequence.Take(2).All(x => x.IsValid));
      }

      [TestMethod]
      public void ScanTest_WhenSequenceWithInvalidCharacters_ThenOneInvalidAllOtersValid()
      {
         var lex = Create();
         var binaries = "101Hugo111110110010000110001111110";

         var sequence = lex.Scan(binaries).ToList();

         Assert.IsFalse(sequence.All(tok => tok.IsValid));
         Assert.AreEqual(1, sequence.Count(tok => !tok.IsValid));
      }

      [TestMethod]
      public void ScanTest_WhenRandomCodeGroupWithTrippelGroups_ThenNoFailedTokens()
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
      public void ScanTest_WhenCodeGroupLengthModuloThreeIsOne_ThenOneFailedTokens()
      {
         var lex = Create();
         var binaries = "0000111100011010100011011000110";

         var sequence = lex.Scan(binaries).ToList();

         Assert.AreEqual(1, sequence.Count(x => x.Type == null));
      }

      [TestMethod]
      public void ScanTest_WhenRandomCodeGroupLengthModuloThreeIsOne_ThenOneFailedTokens()
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
      public void ScanTest_WhenRandomCodeGroupLengthModuloThreeIsTwo_ThenOneFailedTokens()
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

      private ITokenizer<long> Create()
      {
         var lex = LeTokBuilder<long>
            .Create()
            .Register(@"000", 0)
            .Register(@"001", 1)
            .Register(@"010", 2)
            .Register(@"011", 3)
            .Register(@"100", 4)
            .Register(@"101", 5)
            .Register(@"110", 6)
            .Register(@"111", 7)
            .Initialize();
         return lex;
      }
   }
}