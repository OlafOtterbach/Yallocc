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
      public void Parse_Simple_abaa_Correct()
      {
         var lex = CreateAbLex();
         var grammar = CreateSimpleGrammar();
         var sequence = lex.Scan("abaa");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsTrue(result);
      }

      [TestMethod]
      public void Parse_Simple_abba_NotCorrect()
      {
         var lex = CreateAbLex();
         var grammar = CreateSimpleGrammar();
         var sequence = lex.Scan("abba");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result);
      }

      [TestMethod]
      public void Parse_Container_abaa_Correct()
      {
         var lex = CreateAbLex();
         var grammar = CreateContainerGrammar();
         var sequence = lex.Scan("abaa");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsTrue(result);
      }

      [TestMethod]
      public void Parse_Container_abba_NotCorrect()
      {
         var lex = CreateAbLex();
         var grammar = CreateContainerGrammar();
         var sequence = lex.Scan("abba");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result);
      }

      [TestMethod]
      public void Parse_Loop_aaaaab_Correct()
      {
         var lex = CreateAbLex();
         var grammar = CreateLoopGrammar();
         var sequence = lex.Scan("aaaaab");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsTrue(result);
      }

      [TestMethod]
      public void Parse_Loop_aaaaaa_NotCorrect()
      {
         var lex = CreateAbLex();
         var grammar = CreateLoopGrammar();
         var sequence = lex.Scan("aaaaaa");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result);
      }

      [TestMethod]
      public void Parse_Loop_aaaaaaba_NotCorrect()
      {
         var lex = CreateAbLex();
         var grammar = CreateLoopGrammar();
         var sequence = lex.Scan("aaaaaaba");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result);
      }

      [TestMethod]
      public void Parse_Loop_aaaaab_TextIsStartaaaaab()
      {
         var res = new Result();
         var lex = CreateAbLex();
         var grammar = CreateAttributedLoopGrammar(res);
         var sequence = lex.Scan("aaaaab");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsTrue(result);
         Assert.AreEqual(res.Text, "Start:aaaab");
      }

      private Transition CreateSimpleGrammar()
      {
         var first = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         var second = new TokenTypeTransition<AbTokenType>(AbTokenType.b_token);
         var third = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         var fourd = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         first.AddSuccessor(second);
         second.AddSuccessor(third);
         third.AddSuccessor(fourd);
         return first;
      }

      private Transition CreateContainerGrammar()
      {
         var start = CreateSimpleGrammar();
         var container = new GrammarTransition(start);
         return container;
      }

      private Transition CreateLoopGrammar()
      {
         var first = new LabelTransition("Start");
         var secondOne = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         var secondTwo = new TokenTypeTransition<AbTokenType>(AbTokenType.b_token);
         first.AddSuccessor(secondOne);
         first.AddSuccessor(secondTwo);
         secondOne.AddSuccessor(first);
         var container = new GrammarTransition(first);
         return container;
      }

      private class Result
      {
         public string Text;
      }

      private Transition CreateAttributedLoopGrammar(Result result)
      {
         var first = new LabelTransition("Start", () => { result.Text += "Start:"; });
         var secondOne = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token, () => result.Text += "a");
         var secondTwo = new TokenTypeTransition<AbTokenType>(AbTokenType.b_token, () => result.Text += "b");
         first.AddSuccessor(secondOne);
         first.AddSuccessor(secondTwo);
         secondOne.AddSuccessor(first);
         var container = new GrammarTransition(first);
         return container;
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
