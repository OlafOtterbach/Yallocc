using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LexSharp;

namespace ParserLib
{
   internal enum AbTokenType
   {
      a_token,
      b_token,
   }


   [TestClass]
   public class SimpleParserTest
   {
      [TestMethod]
      public void Parse_abaa_Correct()
      {
         var lex = CreateAbLex();
         var grammar = CreateGrammar();
         var sequence = lex.Scan("abaa");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsTrue(result);
      }

      [TestMethod]
      public void Parse_abba_NotCorrect()
      {
         var lex = CreateAbLex();
         var grammar = CreateGrammar();
         var sequence = lex.Scan("abba");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result);
      }

      private Grammar CreateGrammar()
      {
         var first = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         var second = new TokenTypeTransition<AbTokenType>(AbTokenType.b_token);
         var third = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         var fourd = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         first.AddSuccessor(second);
         second.AddSuccessor(third);
         third.AddSuccessor(fourd);
         var grammar = new Grammar(first);
         return grammar;
      }

      private Lex<AbTokenType> CreateAbLex()
      {
         var lex = new Lex<AbTokenType>();
         lex.Register(@"a", AbTokenType.a_token);
         lex.Register(@"b", AbTokenType.b_token);
         return lex;
      }
   }
}
