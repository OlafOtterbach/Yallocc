using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc
{
   [TestClass]
   public class GrammarBuilderTests
   {
      private class DummyContext { }

      private enum AbcTokenType
      {
         a_token,
         b_token,
         c_token,
      }

      [TestMethod]
      public void EmptyGrammarTest_EmptyGrammar_Correct()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);
         b.MasterGrammar("Grammar").Enter.Exit.EndGrammar();

         Assert.IsTrue(Parser("", grammarDictionary.GetMasterGrammar()));
      }

      [TestMethod]
      public void GrammarTest_NoBranches_ParsingTextCorrect()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);
         b.MasterGrammar("Grammar")
          .Enter
          .Token(AbcTokenType.a_token)
          .Token(AbcTokenType.b_token)
          .Exit
          .EndGrammar();

         Assert.IsTrue(Parser("ab", grammarDictionary.GetMasterGrammar()));
      }

      [TestMethod]
      public void GrammarTest_WithBranchAndGotoLoop_ParsingTextCorrect()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);
         b.MasterGrammar("Grammar")
          .Enter
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
          .Exit
          .EndGrammar();

         Assert.IsTrue(Parser("abbbca", grammarDictionary.GetMasterGrammar()));
      }

      [TestMethod]
      public void GrammarTest_WithRecursion_ParsingTextCorrect()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);

         b.Grammar("Container").Enter.Token(AbcTokenType.c_token).Token(AbcTokenType.b_token).Token(AbcTokenType.a_token).Exit.EndGrammar();
         b.MasterGrammar("Grammar")
          .Enter
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
          .Exit
          .EndGrammar();

         Assert.IsTrue(Parser("acbabbbca", grammarDictionary.GetMasterGrammar()));
      }

      [TestMethod]
      public void GotoTest_TargetknownInSameBranch_NotCorrect()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);
         b.MasterGrammar("Grammar")
          .Enter
          .Token(AbcTokenType.a_token)
          .Goto("Target")
          .Token(AbcTokenType.b_token).Name("Target")
          .Exit
          .EndGrammar();

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
             .Enter
             .Token(AbcTokenType.a_token)
             .Goto("Target2")
             .Token(AbcTokenType.b_token).Name("Target")
             .Exit
             .EndGrammar();
         }
         catch (GrammarBuildingException e)
         {
            exeptionThrown = true;
            Assert.AreEqual("Missing target label \"Target2\" for goto command.",e.Message);
            Assert.AreEqual("Target2",e.Label);
            Assert.IsTrue(e.HasUndefinedGotoLabel);
         }

         Assert.IsTrue(exeptionThrown);
      }

      [TestMethod]
      public void GotoTest_TargetInOtherBranch_CorrectParsing()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);
         b.MasterGrammar("Grammar")
          .Enter
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
          .Exit
          .EndGrammar();

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
            b.Grammar("Container").Enter.Token(AbcTokenType.a_token).Name("InvalidTarget").Exit.EndGrammar();
            b.MasterGrammar("Grammar")
             .Enter
             .Token(AbcTokenType.b_token)
             .Goto("InvalidTarget")
             .Gosub("Container")
             .Token(AbcTokenType.c_token)
             .Exit
             .EndGrammar();
         }
         catch (GrammarBuildingException e)
         {
            exeptionThrown = true;
            Assert.AreEqual("Missing target label \"InvalidTarget\" for goto command.",e.Message);
            Assert.AreEqual("InvalidTarget",e.Label);
            Assert.IsTrue(e.HasUndefinedGotoLabel);
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
            b.Grammar("Container").Enter.Token(AbcTokenType.a_token).Goto("InvalidTarget").Exit.EndGrammar();
            b.MasterGrammar("Grammar")
             .Enter
             .Token(AbcTokenType.b_token)
             .Gosub("Container")
             .Token(AbcTokenType.c_token).Name("InvalidTarget")
             .Exit
             .EndGrammar();
         }
         catch (GrammarBuildingException e)
         {
            exeptionThrown = true;
            Assert.AreEqual("Missing target label \"InvalidTarget\" for goto command.",e.Message);
            Assert.AreEqual("InvalidTarget",e.Label);
            Assert.IsTrue(e.HasUndefinedGotoLabel);
         }

         Assert.IsTrue(exeptionThrown);
      }

      [TestMethod]
      public void AnyTokenGrammarTest_AnyTokenInMainAndBranc_ParsingTextCorrect()
      {
         var grammarDictionary = new GrammarDictionary();
         var b = CreateBuilder(grammarDictionary);
         b.MasterGrammar("Grammar")
          .Enter
          .Token(AbcTokenType.a_token)
          .AnyToken()
          .Token(AbcTokenType.b_token)
          .Switch
           (
             b.Branch.Token(AbcTokenType.c_token),
             b.Branch.AnyToken()
           )
          .Exit
          .EndGrammar();

         Assert.IsTrue(Parser("acba", grammarDictionary.GetMasterGrammar()));
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
            .Enter
            .Token(AbcTokenType.c_token).Action((DummyContext ctx, Token < AbcTokenType> tok) => res.Text += tok.Value)
            .Exit
            .EndGrammar();

         b.MasterGrammar("Grammar")
          .Enter
          .Label("Start").Action((DummyContext ctx) => res.Text += "[Start]")
          .Token(AbcTokenType.a_token).Action((DummyContext ctx, Token < AbcTokenType> tok) => res.Text += tok.Value)
          .Lambda.Action((DummyContext ctx) => res.Text += "<Gosub>")
          .Gosub("Container").Action((DummyContext ctx) => res.Text += "<\\Gosub>")
          .Token(AbcTokenType.b_token).Name("Target").Action((DummyContext ctx, Token < AbcTokenType> tok) => res.Text += tok.Value)
          .Label("Loop").Action((DummyContext ctx) => res.Text += "[Loop]")
          .Switch
           (
             b.Branch.Token(AbcTokenType.a_token).Action((DummyContext ctx, Token < AbcTokenType> tok) => res.Text += tok.Value).Goto("Loop"),
             b.Branch.Token(AbcTokenType.c_token).Action((DummyContext ctx, Token < AbcTokenType> tok) => res.Text += tok.Value)
           )
          .Label("End").Action((DummyContext ctx) => res.Text += "[End]")
          .Exit
          .EndGrammar();

         Assert.IsTrue(Parser("acbaaaaaac", grammarDictionary.GetMasterGrammar()));
         string expected = @"[Start]a<Gosub>c<\Gosub>b[Loop]a[Loop]a[Loop]a[Loop]a[Loop]a[Loop]a[Loop]c[End]";
         Assert.AreEqual(expected,res.Text);
      }

      private bool Parser(string text, Transition grammar)
      {
         var tokenizer = CreateAbcLex();
         var parser = new Parser<DummyContext, AbcTokenType>(grammar);

         var sequence = tokenizer.Scan(text);
         var result = parser.ParseTokens(sequence, new DummyContext());

         return result.Success;
      }

      private GrammarBuilderInterface<DummyContext, AbcTokenType> CreateBuilder(GrammarDictionary grammarDictionary)
      {
         var baseBuilder = new GrammarBuilder<DummyContext, AbcTokenType>(grammarDictionary);
         var builderInterface = new GrammarBuilderInterface<DummyContext, AbcTokenType>(baseBuilder);
         return builderInterface;
      }

      private Tokenizer<AbcTokenType> CreateAbcLex()
      {
         var creator = new LeTokCreator<AbcTokenType>();
         creator.Register(@"a", AbcTokenType.a_token);
         creator.Register(@"b", AbcTokenType.b_token);
         creator.Register(@"c", AbcTokenType.c_token);
         return creator.Create();
      }
   }
}
