using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer.LexSharp;

namespace Yallocc.Tokenizer
{
   [TestClass]
   class LexSharpTokenizerTest : TokenizerTest
   {
      protected override TokenizerCreator<AbcTokenType> GetCreator()
      {
         return new LexSharpCreator<AbcTokenType>();
      }
   }
}
