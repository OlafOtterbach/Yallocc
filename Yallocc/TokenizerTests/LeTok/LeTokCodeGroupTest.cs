using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class LeTokCodeGroupTest : CodeGroupTest
   {
      protected override TokenizerCreator<long> GetCreator()
      {
         return new LeTokCreator<long>();
      }
   }
}
