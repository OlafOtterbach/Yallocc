using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Yallocc.Tokenizer.LexSharp;

namespace Yallocc.Tokenizer
{
   [TestClass]
   public class LexSharpTokenizerTest : TokenizerTest
   {
      protected override TokenizerCreator<AbcTokenType> GetCreator()
      {
         return new LexSharpCreator<AbcTokenType>();
      }
   }
}
