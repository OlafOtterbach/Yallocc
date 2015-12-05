using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class LeTokTokenizerTest : TokenizerTest
   {
      protected override TokenizerCreator<AbcTokenType> GetCreator()
      {
         return new LeTokCreator<AbcTokenType>();
      }
   }
}
