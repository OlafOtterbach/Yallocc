using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class LeTokTest
   {
      private enum Numbers
      {
         integer,
         real
      }

      [TestMethod]
      public void ScanTest_WhenLongerRealpatternIsRegisteredFirst_ThenRealValueIsIdentified()
      {
         var text = "123.456";
         var creator = new LeTokCreator<Numbers>();
         creator.Register(@"(0|1|2|3|4|5|6|7|8|9)+\.(0|1|2|3|4|5|6|7|8|9)+", Numbers.real);
         creator.Register(@"(0|1|2|3|4|5|6|7|8|9)+", Numbers.integer);
         var lex = creator.Create();

         var sequence = lex.Scan(text).ToList();

         Assert.AreEqual(1, sequence.Count);
         Assert.IsTrue(sequence.First().Type == Numbers.real);
      }

      /// <summary>
      /// Problem of LeTok because of scanning tokens by regular expression of
      /// connected patterns by or. Cannot match the longest pattern.
      /// You have to sort the patterns.
      /// </summary>
      [TestMethod]
      public void ScanTest_WhenLongerRealpatternIsRegisteredSecond_ThenRealValueIsNotIdentified()
      {
         var text = "123.456";
         var creator = new LeTokCreator<Numbers>();
         creator.Register(@"(0|1|2|3|4|5|6|7|8|9)+", Numbers.integer);
         creator.Register(@"(0|1|2|3|4|5|6|7|8|9)+\.(0|1|2|3|4|5|6|7|8|9)+", Numbers.real);
         var lex = creator.Create();

         var sequence = lex.Scan(text).ToList();

         Assert.AreNotEqual(1, sequence.Count);
         Assert.IsFalse(sequence.First().Type == Numbers.real);
      }
   }
}