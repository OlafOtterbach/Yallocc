using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Yallocc.Tokenizer;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc
{
   internal enum AbTokenType
   {
      a_token,
      b_token,
   }

   [TestClass]
   public class SimpleParserTest
   {
      private class DummyContext {}

      [TestMethod]
      public void AnyTokenTest_abCa_Correct()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateAnyTokenGrammar();
         var sequence = tokenizer.Scan("abCa");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsTrue(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void AnyTokenTest_abba_Correct()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateAnyTokenGrammar();
         var sequence = tokenizer.Scan("abba");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsTrue(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void AnyTokenTest_abaa_Correct()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateAnyTokenGrammar();
         var sequence = tokenizer.Scan("abaa");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsTrue(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void AnyTokenTest_aaCa_NotCorrect()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateAnyTokenGrammar();
         var sequence = tokenizer.Scan("aaCa");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.SyntaxError);
         Assert.IsTrue(result.SyntaxError);
      }

      [TestMethod]
      public void Parse_Simple_abaa_Correct()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateSimpleGrammar();
         var sequence = tokenizer.Scan("abaa");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsTrue(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void Parse_Simple_bbaa_NotCorrect()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateSimpleGrammar();
         var sequence = tokenizer.Scan("bbaa");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
         Assert.AreEqual(0,result.Position);
      }

      [TestMethod]
      public void Parse_Simple_abba_NotCorrect()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateSimpleGrammar();
         var sequence = tokenizer.Scan("abba");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.SyntaxError);
         Assert.AreEqual(2, result.Position);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void Parse_Container_abaa_Correct()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateContainerGrammar();
         var sequence = tokenizer.Scan("abaa").ToList();
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsTrue(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void Parse_Container_abba_NotCorrect()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateContainerGrammar();
         var sequence = tokenizer.Scan("abba");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.SyntaxError);
         Assert.AreEqual(2,result.Position);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void Parse_Loop_aaaaab_Correct()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateLoopGrammar();
         var sequence = tokenizer.Scan("aaaaab");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsTrue(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
      }

      [TestMethod]
      public void Parse_Loop_aaaaaa_NotCorrect()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateLoopGrammar();
         var sequence = tokenizer.Scan("aaaaaa");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsFalse(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsTrue(result.GrammarOfTextNotComplete);
         Assert.AreEqual(5,result.Position);
      }

      [TestMethod]
      public void Parse_Loop_aaaaaaba_NotCorrect()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateLoopGrammar();
         var sequence = tokenizer.Scan("aaaaaaba");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.SyntaxError);
         Assert.IsFalse(result.GrammarOfTextNotComplete);
         Assert.AreEqual(7,result.Position);
      }

      [TestMethod]
      public void Parse_EndlessLoop_aaaaa_NoSuccess()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateEndlessLoopGrammar();
         var sequence = tokenizer.Scan("aaaaa");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.GrammarOfTextNotComplete);
         Assert.AreEqual(4,result.Position);
      }

      [TestMethod]
      public void Parse_NotDeterministicBranchLoop_aaaaa_NoSuccess()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateNotDeterministicBranchLoop();
         var sequence = tokenizer.Scan("aaaaa");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsFalse(result.Success);
      }

      [TestMethod]
      public void Parse_CreateNotDeterministicDeadLoopBranch_a_Correct()
      {
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateDeadLoopBranch();
         var sequence = tokenizer.Scan("a");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsFalse(result.Success);
         Assert.IsTrue(result.GrammarOfTextNotComplete);
         Assert.AreEqual(0,result.Position);
      }

      [TestMethod]
      public void Parse_Loop_aaaaab_TextIsStartaaaaab()
      {
         var res = new Result();
         var tokenizer = CreateAbTokenizer();
         var grammar = CreateAttributedLoopGrammar(res);
         var sequence = tokenizer.Scan("aaaaab");
         var Parser = new Parser<DummyContext, AbTokenType>(grammar);

         var result = Parser.ParseTokens(sequence, new DummyContext());

         Assert.IsTrue(result.Success);
         Assert.AreEqual("[Start][Label]a[Label]a[Label]a[Label]a[Label]a[Label]b",res.Text);
      }

      private Transition CreateSimpleGrammar()
      {
         var first = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
         var second = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.b_token);
         var third = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
         var fourd = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
         first.AddSuccessor(second);
         second.AddSuccessor(third);
         third.AddSuccessor(fourd);
         return first;
      }

      private Transition CreateContainerGrammar()
      {
         var start = CreateSimpleGrammar();
         var container = new GrammarTransition<DummyContext>(start);
         return container;
      }

      private Transition CreateLoopGrammar()
      {
         var first = new LabelTransition<DummyContext>("Start");
         var secondOne = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
         var secondTwo = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.b_token);
         first.AddSuccessor(secondOne);
         first.AddSuccessor(secondTwo);
         secondOne.AddSuccessor(first);
         var container = new GrammarTransition<DummyContext>(first);
         return container;
      }

      private Transition CreateEndlessLoopGrammar()
      {
         var first = new LabelTransition<DummyContext>("Start");
         var second = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
         first.AddSuccessor(second);
         second.AddSuccessor(first);
         return first;
      }

      private Transition CreateNotDeterministicBranchLoop()
      {
         var first = new LabelTransition<DummyContext>("Start");
         var secondOne = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
         var secondTwo = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
         first.AddSuccessor(secondOne);
         first.AddSuccessor(secondTwo);
         secondOne.AddSuccessor(first);
         return first;
      }

      private Transition CreateDeadLoopBranch()
      {
         var first = new LabelTransition<DummyContext>("Start");
         var secondOne = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
         var secondTwo = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
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
         var first = new LabelTransition<DummyContext>("Start"){ Action = (DummyContext ctx) => { result.Text += "[Label]"; } };
         var secondOne = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token) { Action = (DummyContext ctx, Token<AbTokenType> token) => result.Text += token.Value };
         var secondTwo = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.b_token) { Action = (DummyContext ctx, Token<AbTokenType> token) => result.Text += token.Value };
         first.AddSuccessor(secondOne);
         first.AddSuccessor(secondTwo);
         secondOne.AddSuccessor(first);
         var container = new GrammarTransition<DummyContext>(first){ Action = (DummyContext ctx) => { result.Text += "[Start]"; } };
         return container;
      }

      private Transition CreateAnyTokenGrammar()
      {
         var first = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
         var second = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.b_token);
         var third = new AnyTokenTypeTransition<DummyContext, AbTokenType>();
         var fourd = new TokenTypeTransition<DummyContext, AbTokenType>(AbTokenType.a_token);
         first.AddSuccessor(second);
         second.AddSuccessor(third);
         third.AddSuccessor(fourd);
         return first;
      }

      private Tokenizer<AbTokenType> CreateAbTokenizer()
      {
         var creator = new LeTokCreator<AbTokenType>();
         creator.Register(@"a", AbTokenType.a_token);
         creator.Register(@"b", AbTokenType.b_token);
         var tokenizer = creator.Create();
         return tokenizer;
      }
   }
}
