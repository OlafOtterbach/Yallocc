using System.Linq;
using LexSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YalloccDemo;
using YalloccDemo.Basic;

namespace YalloccDemoTest
{
   [TestClass]
   public class BasicArrayTest
   {
      [TestMethod]
      public void GetSetTest_OneDimensions_ReadEqualsWrittenValue()
      {
         var array = new BasicArray(5);

         array.Set(new BasicInteger(77), 3);
         var val = array.Get(3) as BasicInteger;

         Assert.IsNotNull(val);
         Assert.AreEqual(77, val.Value);
      }

      [TestMethod]
      public void GetSetTest_TwoDimensions_ReadEqualsWrittenValue()
      {
         var array = new BasicArray(5,5);

         array.Set(new BasicInteger(77), 3,4);
         var val = array.Get(3,4) as BasicInteger;

         Assert.IsNotNull(val);
         Assert.AreEqual(77, val.Value);
      }

      [TestMethod]
      public void GetSetTest_ThreeDimensions_ReadEqualsWrittenValue()
      {
         var array = new BasicArray(5, 5, 5);

         array.Set(new BasicInteger(77), 3, 4, 2);
         var val = array.Get(3, 4, 2) as BasicInteger;

         Assert.IsNotNull(val);
         Assert.AreEqual(77, val.Value);
      }

      [TestMethod]
      public void GetSetTest_FilledArray_ReadEqualsWrittenValue()
      {
         var array = new BasicArray(5, 5, 5);
         int counter = 1;
         for (int x = 0; x < 5; x++)
            for (int y = 0; x < 5; x++)
               for (int z = 0; x < 5; x++)
                  array.Set(new BasicInteger(counter++), x, y, z);

         counter = 1;
         for (int x = 0; x < 5; x++)
         {
            for (int y = 0; x < 5; x++)
            {
               for (int z = 0; x < 5; x++)
               {
                  var actual = (array.Get(x, y, z) as BasicInteger).Value;
                  Assert.AreEqual(counter, actual);
                  counter++;
               }
            }
         }
      }
   }
}