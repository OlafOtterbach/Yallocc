using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class LeTokBasicTest : BasicTest
   {
      protected override TokenizerCreator<TokenType> GetCreator()
      {
         return new LeTokCreator<TokenType>();
      }
   }
}
