﻿using LexSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

         Assert.IsTrue(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void Parse_Simple_bbaa_NotCorrect()
      {
         var lex = CreateAbLex();
         var grammar = CreateSimpleGrammar();
         var sequence = lex.Scan("bbaa");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
         Assert.AreEqual(result.Position, 0);
      }

      [TestMethod]
      public void Parse_Simple_abba_NotCorrect()
      {
         var lex = CreateAbLex();
         var grammar = CreateSimpleGrammar();
         var sequence = lex.Scan("abba");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.SyntaxError);
         Assert.AreEqual(result.Position, 2);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void Parse_Container_abaa_Correct()
      {
         var lex = CreateAbLex();
         var grammar = CreateContainerGrammar();
         var sequence = lex.Scan("abaa").ToList();
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsTrue(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void Parse_Container_abba_NotCorrect()
      {
         var lex = CreateAbLex();
         var grammar = CreateContainerGrammar();
         var sequence = lex.Scan("abba");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.SyntaxError);
         Assert.AreEqual(result.Position, 2);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void Parse_Loop_aaaaab_Correct()
      {
         var lex = CreateAbLex();
         var grammar = CreateLoopGrammar();
         var sequence = lex.Scan("aaaaab");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsTrue(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void Parse_Loop_aaaaaa_NotCorrect()
      {
         var lex = CreateAbLex();
         var grammar = CreateLoopGrammar();
         var sequence = lex.Scan("aaaaaa");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsTrue(result.GrammarOfTextNotComplete);
         Assert.AreEqual(result.Position, 5);
      }

      [TestMethod]
      public void Parse_Loop_aaaaaaba_NotCorrect()
      {
         var lex = CreateAbLex();
         var grammar = CreateLoopGrammar();
         var sequence = lex.Scan("aaaaaaba");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
         Assert.AreEqual(result.Position, 7);
      }

      [TestMethod]
      public void Parse_EndlessLoop_aaaaa_NoSuccess()
      {
         var lex = CreateAbLex();
         var grammar = CreateEndlessLoopGrammar();
         var sequence = lex.Scan("aaaaa");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.GrammarOfTextNotComplete);
         Assert.AreEqual(result.Position, 4);
      }

      [TestMethod]
      public void Parse_NotDeterministicBranchLoop_aaaaa_NoSuccess()
      {
         var lex = CreateAbLex();
         var grammar = CreateNotDeterministicBranchLoop();
         var sequence = lex.Scan("aaaaa");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result.Success);
      }

      [TestMethod]
      public void Parse_CreateNotDeterministicDeadLoopBranch_a_Correct()
      {
         var lex = CreateAbLex();
         var grammar = CreateDeadLoopBranch();
         var sequence = lex.Scan("a");
         var parser = new Parser<AbTokenType>();

         var result = parser.ParseTokens(grammar, sequence);

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.GrammarOfTextNotComplete);
         Assert.AreEqual(result.Position, 0);
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

         Assert.IsTrue(result.Success);
         Assert.AreEqual(res.Text, "[Start][Label]a[Label]a[Label]a[Label]a[Label]a[Label]b");
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

      private Transition CreateEndlessLoopGrammar()
      {
         var first = new LabelTransition("Start");
         var second = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         first.AddSuccessor(second);
         second.AddSuccessor(first);
         return first;
      }

      private Transition CreateNotDeterministicBranchLoop()
      {
         var first = new LabelTransition("Start");
         var secondOne = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         var secondTwo = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         first.AddSuccessor(secondOne);
         first.AddSuccessor(secondTwo);
         secondOne.AddSuccessor(first);
         return first;
      }

      private Transition CreateDeadLoopBranch()
      {
         var first = new LabelTransition("Start");
         var secondOne = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         var secondTwo = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token);
         first.AddSuccessor(secondOne);
         first.AddSuccessor(secondTwo);
         secondOne.AddSuccessor(secondOne);
         return first;
      }

      private class Result
      {
         public string Text;
      }

      private Transition CreateAttributedLoopGrammar(Result result)
      {
         var first = new LabelTransition("Start"){ Action = () => { result.Text += "[Label]"; } };
         var secondOne = new TokenTypeTransition<AbTokenType>(AbTokenType.a_token) { Action = (Token<AbTokenType> token) => result.Text += token.Value };
         var secondTwo = new TokenTypeTransition<AbTokenType>(AbTokenType.b_token) { Action = (Token<AbTokenType> token) => result.Text += token.Value };
         first.AddSuccessor(secondOne);
         first.AddSuccessor(secondTwo);
         secondOne.AddSuccessor(first);
         var container = new GrammarTransition(first){ Action = () => { result.Text += "[Start]"; } };
         return container;
      }

      private LexSharp<AbTokenType> CreateAbLex()
      {
         var lex = new LexSharp<AbTokenType>();
         lex.Register(@"a", AbTokenType.a_token);
         lex.Register(@"b", AbTokenType.b_token);
         return lex;
      }
   }
}
