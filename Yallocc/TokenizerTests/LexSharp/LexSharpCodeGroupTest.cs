using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer.LexSharp;

namespace Yallocc.Tokenizer
{
   [TestClass]
   class LexSharpCodeGroupTest : CodeGroupTest
   {
      protected override TokenizerCreator<long> GetCreator()
      {
         return new LexSharpCreator<long>();
      }
   }
}
