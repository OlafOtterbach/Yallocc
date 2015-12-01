using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class LeTokTokenTypeTest : TokenTypeTest
   {
      protected override TokenizerCreator<TokenType> GetCreator()
      {
         return new LeTokCreator<TokenType>();
      }
   }
}
