using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer.LexSharp;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class LexSharpBasicTest : BasicTest
   {
      protected override TokenizerCreator<TokenType> GetCreator()
      {
         return new LexSharpCreator<TokenType>();
      }
   }
}
