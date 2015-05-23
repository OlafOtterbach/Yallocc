using LexSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yallocc
{
   [TestClass]
   public class GrammarBuilderTests
   {
      private enum AbcTokenType
      {
         a_token,
         b_token,
         c_token,
      }

      [TestMethod]
      public void GrammarTest_NoBranches_ParsingTextCorrect()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);
         b.MasterGrammar("Grammar")
          .Begin
          .Token(AbcTokenType.a_token)
          .Token(AbcTokenType.b_token)
          .End();

         Assert.IsTrue(Parser("ab", grammarDictionary.GetMasterGrammar()));
      }

      [TestMethod]
      public void GrammarTest_WithBranchAndGotoLoop_ParsingTextCorrect()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);
         b.MasterGrammar("Grammar")
          .Begin
          .Token(AbcTokenType.a_token)
          .Label("JumpIn")
          .Switch
           (
              b.Branch
               .Token(AbcTokenType.b_token)
               .Goto("JumpIn"),
              b.Branch
               .Token(AbcTokenType.c_token)
           )
          .Token(AbcTokenType.a_token)
          .End();

         Assert.IsTrue(Parser("abbbca", grammarDictionary.GetMasterGrammar()));
      }

      [TestMethod]
      public void GrammarTest_WithRecursion_ParsingTextCorrect()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);

         b.Grammar("Container").Begin.Token(AbcTokenType.c_token).Token(AbcTokenType.b_token).Token(AbcTokenType.a_token).End();
         b.MasterGrammar("Grammar")
          .Begin
          .Token(AbcTokenType.a_token)
          .Gosub("Container")
          .Label("JumpIn")
          .Switch
           (
              b.Branch
               .Token(AbcTokenType.b_token)
               .Goto("JumpIn"),
              b.Branch
               .Token(AbcTokenType.c_token)
           )
          .Token(AbcTokenType.a_token)
          .End();

         Assert.IsTrue(Parser("acbabbbca", grammarDictionary.GetMasterGrammar()));
      }

      [TestMethod]
      public void GotoTest_TargetknownInSameBranch_NotCorrect()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);
         b.MasterGrammar("Grammar")
          .Begin
          .Token(AbcTokenType.a_token)
          .Goto("Target")
          .Token(AbcTokenType.b_token).Name("Target")
          .End();

         Assert.IsTrue(Parser("ab", grammarDictionary.GetMasterGrammar()));
      }

      [TestMethod]
      public void GotoTest_TargetUnknownInSameBranch_NotCorrect()
      {
         bool exeptionThrown = false;
         try
         {
            var grammarDictionary = new GrammarDictionary();
            var b = CreateBuilder(grammarDictionary);
            b.MasterGrammar("Grammar")
             .Begin
             .Token(AbcTokenType.a_token)
             .Goto("Target2")
             .Token(AbcTokenType.b_token).Name("Target")
             .End();
         }
         catch (MissingGotoLabelException e)
         {
            exeptionThrown = true;
            Assert.AreEqual(e.Message, "Missing target label \"Target2\" for goto command.");
            Assert.AreEqual(e.Label, "Target2");
         }

         Assert.IsTrue(exeptionThrown);
      }

      [TestMethod]
      public void GotoTest_TargetInOtherBranch_CorrectParsing()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);
         b.MasterGrammar("Grammar")
          .Begin
          .Token(AbcTokenType.c_token)
          .Switch
           (
              b.Branch
               .Token(AbcTokenType.a_token)
               .Goto("Two")
               .Token(AbcTokenType.b_token).Name("One"),
              b.Branch
               .Token(AbcTokenType.b_token)
               .Goto("One")
               .Token(AbcTokenType.a_token).Name("Two")
           )
          .Token(AbcTokenType.c_token)
          .End();

         Assert.IsTrue(Parser("caac", grammarDictionary.GetMasterGrammar()));
         Assert.IsTrue(Parser("cbbc", grammarDictionary.GetMasterGrammar()));
      }

      [TestMethod]
      public void GotoTest_TargetInSubGrammar_ParsingTextNotCorrect()
      {
         bool exeptionThrown = false;
         try
         {
            var grammarDictionary = new GrammarDictionary();
            var b = CreateBuilder(grammarDictionary);
            b.Grammar("Container").Begin.Token(AbcTokenType.a_token).Name("InvalidTarget").End();
            b.MasterGrammar("Grammar")
             .Begin
             .Token(AbcTokenType.b_token)
             .Goto("InvalidTarget")
             .Gosub("Container")
             .Token(AbcTokenType.c_token)
             .End();
         }
         catch (MissingGotoLabelException e)
         {
            exeptionThrown = true;
            Assert.AreEqual(e.Message, "Missing target label \"InvalidTarget\" for goto command.");
            Assert.AreEqual(e.Label, "InvalidTarget");
         }

         Assert.IsTrue(exeptionThrown);
      }

      [TestMethod]
      public void GotoTest_JumpFromSubGrammar_ParsingTextNotCorrect()
      {
         bool exeptionThrown = false;
         try
         {
            var grammarDictionary = new GrammarDictionary();
            var b = CreateBuilder(grammarDictionary);
            b.Grammar("Container").Begin.Token(AbcTokenType.a_token).Goto("InvalidTarget").End();
            b.MasterGrammar("Grammar")
             .Begin
             .Token(AbcTokenType.b_token)
             .Gosub("Container")
             .Token(AbcTokenType.c_token).Name("InvalidTarget")
             .End();
         }
         catch (MissingGotoLabelException e)
         {
            exeptionThrown = true;
            Assert.AreEqual(e.Message, "Missing target label \"InvalidTarget\" for goto command.");
            Assert.AreEqual(e.Label, "InvalidTarget");
         }

         Assert.IsTrue(exeptionThrown);
      }

      private class Result
      {
         public string Text;
      }

      [TestMethod]
      public void ActionTest()
      {
         var res = new Result();
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);

         b.Grammar("Container")
            .Begin
            .Token(AbcTokenType.c_token).Action((Token<AbcTokenType> tok) => res.Text += tok.Value)
            .End();

         b.MasterGrammar("Grammar")
          .Begin
          .Label("Start").Action(() => res.Text += "[Start]")
          .Token(AbcTokenType.a_token).Action((Token<AbcTokenType> tok) => res.Text += tok.Value)
          .Gosub("Container").Action(()=>res.Text += "<Gosub>")
          .Lambda(() => res.Text += "<\\Gosub>")
          .Token(AbcTokenType.b_token).Name("Target").Action((Token<AbcTokenType> tok) => res.Text += tok.Value)
          .Label("Loop").Action(() => res.Text += "[Loop]")
          .Switch
           (
             b.Branch.Token(AbcTokenType.a_token).Action((Token<AbcTokenType> tok) => res.Text += tok.Value).Goto("Loop"),
             b.Branch.Token(AbcTokenType.c_token).Action((Token<AbcTokenType> tok) => res.Text += tok.Value)
           )
          .Label("End").Action(() => res.Text += "[End]")
          .End();

         Assert.IsTrue(Parser("acbaaaaaac", grammarDictionary.GetMasterGrammar()));
         string expected = @"[Start]a<Gosub>c<\Gosub>b[Loop]a[Loop]a[Loop]a[Loop]a[Loop]a[Loop]a[Loop]c[End]";
         Assert.AreEqual(res.Text, expected);
      }

      private bool Parser(string text, Transition grammar)
      {
         var lex = CreateAbcLex();
         var parser = new Parser<AbcTokenType>();

         var sequence = lex.Scan(text);
         var result = parser.ParseTokens(grammar, sequence);

         return result.Success;
      }

      private BuilderInterface<AbcTokenType> CreateBuilder(GrammarDictionary grammarDictionary)
      {
         var baseBuilder = new GrammarBuilder<AbcTokenType>(grammarDictionary);
         var builderInterface = new BuilderInterface<AbcTokenType>(baseBuilder);
         return builderInterface;
      }

      private LexSharp<AbcTokenType> CreateAbcLex()
      {
         var lex = new LexSharp<AbcTokenType>();
         lex.Register(@"a", AbcTokenType.a_token);
         lex.Register(@"b", AbcTokenType.b_token);
         lex.Register(@"c", AbcTokenType.c_token);
         return lex;
      }
   }
}
