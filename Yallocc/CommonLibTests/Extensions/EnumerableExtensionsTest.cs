using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Yallocc.CommonLib
{
   [TestClass]
   public class TestSplitForListWithNotEmptySegments
   {
      private List<List<int>> mSegments;

      [TestInitialize]
      public void TestInitialize()
      {
         var elems = new int[] { 1, 2, 3, 4, 5, 3, 6, 7, 8, 9, 3, 2, 1 };
         var segments = elems.Split(num => num == 3).Select(c => c.ToList()).ToList();

         mSegments = segments;
      }

      [TestMethod]
      public void CheckIfNotEmpty()
      {
         Assert.IsTrue(mSegments.Any());
      }

      [TestMethod]
      public void CheckIfCountIsFour()
      {
         Assert.AreEqual(4, mSegments.Count());
      }

      [TestMethod]
      public void CheckIfFirstSegmentHasTwoElements()
      {
         Assert.AreEqual(2, mSegments[0].Count);
      }

      [TestMethod]
      public void CheckIfFirstSequenceIsOneAndTwo()
      {
         Assert.AreEqual(1, mSegments[0][0]);
         Assert.AreEqual(2, mSegments[0][1]);
      }

      [TestMethod]
      public void CheckIfSecondSegmentHasTwoElements()
      {
         Assert.AreEqual(2, mSegments[1].Count);
      }

      [TestMethod]
      public void CheckIfSecondSequenceIsOneAndTwo()
      {
         Assert.AreEqual(4, mSegments[1][0]);
         Assert.AreEqual(5, mSegments[1][1]);
      }

      [TestMethod]
      public void CheckIfThirdSegmentHasTwoElements()
      {
         Assert.AreEqual(4, mSegments[2].Count);
      }

      [TestMethod]
      public void CheckIfThirdSequenceIsOneAndTwo()
      {
         Assert.AreEqual(6, mSegments[2][0]);
         Assert.AreEqual(7, mSegments[2][1]);
         Assert.AreEqual(8, mSegments[2][2]);
         Assert.AreEqual(9, mSegments[2][3]);
      }

      [TestMethod]
      public void CheckIfFourthSegmentHasTwoElements()
      {
         Assert.AreEqual(2, mSegments[3].Count);
      }

      [TestMethod]
      public void CheckIfFourthSequenceIsOneAndTwo()
      {
         Assert.AreEqual(2, mSegments[3][0]);
         Assert.AreEqual(1, mSegments[3][1]);
      }
   }

   [TestClass]
   public class TestSplitForListWithSeparatorAtEndOfList
   {
      private List<List<int>> mSegments;

      [TestInitialize]
      public void TestInitialize()
      {
         var elems = new int[] { 1, 2, 3, 4, 5, 3 };
         var segments = elems.Split(num => num == 3).Select(c => c.ToList()).ToList();

         mSegments = segments;
      }

      [TestMethod]
      public void CheckIfNotEmpty()
      {
         Assert.IsTrue(mSegments.Any());
      }

      [TestMethod]
      public void CheckIfCountIsFour()
      {
         Assert.AreEqual(2, mSegments.Count());
      }

      [TestMethod]
      public void CheckIfFirstSegmentHasTwoElements()
      {
         Assert.AreEqual(2, mSegments[0].Count);
      }

      [TestMethod]
      public void CheckIfFirstSequenceIsOneAndTwo()
      {
         Assert.AreEqual(1, mSegments[0][0]);
         Assert.AreEqual(2, mSegments[0][1]);
      }

      [TestMethod]
      public void CheckIfSecondSegmentHasTwoElements()
      {
         Assert.AreEqual(2, mSegments[1].Count);
      }

      [TestMethod]
      public void CheckIfSecondSequenceIsOneAndTwo()
      {
         Assert.AreEqual(4, mSegments[1][0]);
         Assert.AreEqual(5, mSegments[1][1]);
      }
   }

   [TestClass]
   public class TestSplitForListWithSeparatorAtStartOfList
   {
      private List<List<int>> mSegments;

      [TestInitialize]
      public void TestInitialize()
      {
         var elems = new int[] { 3, 1, 2, 3, 4, 5 };
         var segments = elems.Split(num => num == 3).Select(c => c.ToList()).ToList();

         mSegments = segments;
      }

      [TestMethod]
      public void CheckIfNotEmpty()
      {
         Assert.IsTrue(mSegments.Any());
      }
   }

   [TestClass]
   public class TestSplitForEmptyList
   {
      private List<List<int>> mSegments;

      [TestInitialize]
      public void TestInitialize()
      {
         var elems = new int[0];
         var segments = elems.Split(num => num == 3).Select(c => c.ToList()).ToList();

         mSegments = segments;
      }

      [TestMethod]
      public void CheckIfEmpty()
      {
         Assert.IsFalse(mSegments.Any());
      }
   }

   [TestClass]
   public class TestSplitForListWithEmptySegments
   {
      private List<List<int>> mSegments;

      [TestInitialize]
      public void TestInitialize()
      {
         var elems = new int[] { 3, 3, 3, 3 };
         var segments = elems.Split(num => num == 3).Select(c => c.ToList()).ToList();

         mSegments = segments;
      }

      [TestMethod]
      public void CheckIfIsEmpty()
      {
         Assert.IsFalse(mSegments.Any());
      }
   }
}