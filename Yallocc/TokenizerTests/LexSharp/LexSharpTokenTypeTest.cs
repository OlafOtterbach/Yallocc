using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer.LexSharp;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class LexSharpTokenTypeTest : TokenTypeTest
   {
      protected override TokenizerCreator<TokenType> GetCreator()
      {
         return new LexSharpCreator<TokenType>();
      }
   }
}
