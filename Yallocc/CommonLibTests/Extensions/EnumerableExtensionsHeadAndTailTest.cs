using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Yallocc.CommonLib
{
   [TestClass]
   public class EnumerableExtensionsHeadAndTailTest
   {
      [TestMethod]
      public void WhenSplit_TheFirstIsOneAndFirstOfTailIsTwo()
      {
         var indices = Enumerable.Range(1, int.MaxValue);

         var headerAndTail = indices.SplitHeaderAndTail();

         Assert.AreEqual(1, headerAndTail.Header);
         Assert.AreEqual(2, headerAndTail.Tail.FirstOrDefault());
      }

      [TestMethod]
      public void WhenSplitThreeTimes_ThenHeadersAreOneTwoThree()
      {
         var indices = Enumerable.Range(1, int.MaxValue);

         var firstSplit = indices.SplitHeaderAndTail();
         var secondSplit = firstSplit.Tail.SplitHeaderAndTail();
         var thirdSplit = secondSplit.Tail.SplitHeaderAndTail();

         Assert.AreEqual(1, firstSplit.Header);
         Assert.AreEqual(2, secondSplit.Header);
         Assert.AreEqual(3, thirdSplit.Header);
         Assert.AreEqual(4, thirdSplit.Tail.FirstOrDefault());
      }

      [TestMethod]
      public void WhenEnumarableIsEmpty_ThenHeaderIsDefaultAndTailIsEmpty()
      {
         var indices = new List<int>();

         var headerAndTail = indices.SplitHeaderAndTail();

         Assert.AreEqual(0, headerAndTail.Header);
         Assert.IsFalse(headerAndTail.Tail.Any());
      }
   }
}