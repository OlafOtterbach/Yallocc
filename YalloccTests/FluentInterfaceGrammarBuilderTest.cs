﻿using LexSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yallocc
{
   [TestClass]
   public class FluentInterfaceGrammarBuilderTest
   {
      private enum AbcTokenType
      {
         a_token,
         b_token,
         c_token,
      }

      [TestMethod]
      public void CompileTimeFluentInterfaceTest_AllPossibleBranches_AllwaysTrue()
      {
         var b = CreateBuilder();
         b.Grammar("Grammar")
          .Begin
          .Token(AbcTokenType.a_token).Action((Token<AbcTokenType> tok) => {}).Name("eins")
          .Token(AbcTokenType.b_token).Name("zwei").Action((Token<AbcTokenType> tok) => { })
          .Switch(b.Branch.Token(AbcTokenType.a_token).Action((Token<AbcTokenType> tok) => { }).Name("eins")
                          .Token(AbcTokenType.b_token).Name("zwei").Action((Token<AbcTokenType> tok) => { })
                 )
          .End();

         Assert.IsTrue(true);
      }

      private BuilderInterface<AbcTokenType> CreateBuilder()
      {
         var grammarDictionary = new GrammarDictionary();
         var baseBuilder = new GrammarBuilder<AbcTokenType>(grammarDictionary);
         var builderInterface = new BuilderInterface<AbcTokenType>(baseBuilder);
         return builderInterface;
      }
   }
}