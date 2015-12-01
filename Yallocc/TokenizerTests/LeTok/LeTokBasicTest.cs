﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc.Tokenizer
{
   [TestClass]
   class LeTokBasicTest : BasicTest
   {
      protected override TokenizerCreator<TokenType> GetCreator()
      {
         return new LeTokCreator<TokenType>();
      }
   }
}
