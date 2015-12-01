using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc.Tokenizer
{
   [TestClass]
   class LeTokTokenizerTest : TokenizerTest
   {
      protected override TokenizerCreator<AbcTokenType> GetCreator()
      {
         return new LeTokCreator<AbcTokenType>();
      }
   }
}
