using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer;
using Yallocc.Tokenizer.LeTok;
using Yallocc.Tokenizer.LexSharp;

namespace Yallocc
{
   [TestClass]
   public class YalloccTest
   {
      private enum Token
      {
         A,
         B,
         C,
      }

      private class DummyContext {}

      [TestMethod]
      public void SwitchTest()
      {
         var yacc = new Yallocc<DummyContext, Token>(new LexSharpCreator<Token>());
         yacc.DefineTokens()
             .AddTokenPattern(@"a", Token.A)
             .AddTokenPattern(@"b", Token.B)
             .AddTokenPattern(@"c", Token.C)
             .End();

         yacc.MasterGrammar("MasterGrammar")
             .Enter
             .Switch
              (
                 yacc.Branch.Token(Token.A),
                 yacc.Branch.Token(Token.B),
                 yacc.Branch.Token(Token.C)
              )
             .Exit
             .EndGrammar();

         var parser = yacc.CreateParser();

         var res1 = parser.Parse("a", new DummyContext());

         Assert.IsTrue(res1.Success);
      }

      [TestMethod]
      public void RecursionTest()
      {
         var yacc = new Yallocc<DummyContext, Token>(new LeTokCreator<Token>());
         yacc.DefineTokens()
             .AddTokenPattern(@"a", Token.A)
             .AddTokenPattern(@"b", Token.B)
             .AddTokenPattern(@"c", Token.C)
             .End();

         yacc.MasterGrammar("MasterGrammar")
             .Enter
             .Label("MasterGrammar")
             .Token(Token.A)
             .Gosub("Grammar")
             .Token(Token.A)
             .Exit
             .EndGrammar();

         yacc.Grammar("Grammar")
             .Enter
             .Label("Grammar")
             .Switch
              (
                 yacc.Branch.Token(Token.B),
                 yacc.Branch
                     .Token(Token.C)
                     .Gosub("MasterGrammar")
                     .Token(Token.C)
              )
             .Exit
             .EndGrammar();

         var parser = yacc.CreateParser();

         var res1 = parser.Parse("aba", new DummyContext());

         Assert.IsTrue(res1.Success);
      }

      [TestMethod]
      public void AddTokenAndCreateParserTest_AddABC_NoException()
      {
         var yacc = new Yallocc<DummyContext, Token>(new LexSharpCreator<Token>());
         yacc.DefineTokens()
         .AddTokenPattern(@"a", Token.A)
         .AddTokenPattern(@"b", Token.B)
         .AddTokenPattern(@"c", Token.C)
         .End();

         yacc.MasterGrammar("Grammar").Enter.Exit.EndGrammar();
         var parser = yacc.CreateParser();

         Assert.AreNotEqual(null, parser);
      }

      [TestMethod]
      public void AddTokenAndCreateParserTest_AddAB_NotCompleteException()
      {
         var exceptionFound = false;
         var yacc = new Yallocc<DummyContext, Token>(new LeTokCreator<Token>());
         yacc.DefineTokens()
         .AddTokenPattern(@"a", Token.A)
         .AddTokenPattern(@"b", Token.B)
         .End();

         yacc.MasterGrammar("Grammar").Enter.Exit.EndGrammar();
         try
         {
            var parser = yacc.CreateParser();
         }
         catch (MissingTokenDefinitionException e)
         {
            Assert.AreEqual("Not all types of tokens are defined.", e.Message);
            exceptionFound = true;
         }

         Assert.IsTrue(exceptionFound);
      }

      [TestMethod]
      public void GrammarAndCreateParserTest_NoMasterGrammar_NoMasterGrammarException()
      {
         var exceptionFound = false;
         var yacc = new Yallocc<DummyContext, Token>(new LexSharpCreator<Token>());
         yacc.DefineTokens()
         .AddTokenPattern(@"a", Token.A)
         .AddTokenPattern(@"b", Token.B)
         .AddTokenPattern(@"b", Token.C)
         .End();

         yacc.Grammar("Grammar").Enter.Exit.EndGrammar();
         try
         {
            var parser = yacc.CreateParser();
         }
         catch (GrammarBuildingException e)
         {
            Assert.AreEqual("Master grammar is not defined.", e.Message);
            Assert.IsTrue(e.MasterGrammarIsNotDefined);
            exceptionFound = true;
         }

         Assert.IsTrue(exceptionFound);
      }

      [TestMethod]
      public void GrammarAndCreateParserTest_GrammarAndMasterGrammar_NoException()
      {
         var yacc = new Yallocc<DummyContext, Token>(new LexSharpCreator<Token>());
         yacc.DefineTokens()
         .AddTokenPattern(@"a", Token.A)
         .AddTokenPattern(@"b", Token.B)
         .AddTokenPattern(@"b", Token.C)
         .End();

         yacc.Grammar("Grammar").Enter.Exit.EndGrammar();
         yacc.MasterGrammar("MasterGrammar").Enter.Exit.EndGrammar();
         var parser = yacc.CreateParser();

         Assert.IsTrue(parser.Parse("", new DummyContext()).Success);
      }

      [TestMethod]
      public void MasterGrammar_MasterGrammarDefinedTwice_MasterGrammarAlreadyDefinedException()
      {
         var exceptionFound = false;
         var yacc = new Yallocc<DummyContext, Token>(new LeTokCreator<Token>());
         yacc.DefineTokens()
         .AddTokenPattern(@"a", Token.A)
         .AddTokenPattern(@"b", Token.B)
         .AddTokenPattern(@"b", Token.C)
         .End();

         try
         {
            yacc.MasterGrammar("MasterGrammar1").Enter.Exit.EndGrammar();
            yacc.MasterGrammar("MasterGrammar2").Enter.Exit.EndGrammar();
         }
         catch (GrammarBuildingException e)
         {
            Assert.AreEqual("Master grammar already defined.", e.Message);
            Assert.IsTrue(e.MasterGrammarAlreadyDefined);
            exceptionFound = true;
         }

         Assert.IsTrue(exceptionFound);
      }

      [TestMethod]
      public void CreateParserTest_MasterGrammarWithWrongLinkToGrammar_HasUndefinedSubGrammarException()
      {
         var exceptionFound = false;
         var yacc = new Yallocc<DummyContext, Token>(new LeTokCreator<Token>());
         yacc.DefineTokens()
         .AddTokenPattern(@"a", Token.A)
         .AddTokenPattern(@"b", Token.B)
         .AddTokenPattern(@"b", Token.C)
         .End();

         yacc.MasterGrammar("Grammar")
             .Enter
          .Gosub("NoWhere")
          .Exit
          .EndGrammar();
         try
         {
            var parser = yacc.CreateParser();
         }
         catch (GrammarBuildingException e)
         {
            Assert.AreEqual("Not all subgrammars are defined", e.Message);
            Assert.IsTrue(e.HasUndefinedSubgrammars);
            exceptionFound = true;
         }

         Assert.IsTrue(exceptionFound);
      }

      [TestMethod]
      public void CreateParserTest_MasterGrammarWithNestedGrammarWithWrongLinkToGrammar_HasUndefinedSubGrammarException()
      {
         var exceptionFound = false;
         var yacc = new Yallocc<DummyContext, Token>(new LeTokCreator<Token>());
         yacc.DefineTokens()
         .AddTokenPattern(@"a", Token.A)
         .AddTokenPattern(@"b", Token.B)
         .AddTokenPattern(@"b", Token.C)
         .End();

         yacc.MasterGrammar("MasterGrammar")
             .Enter
             .Gosub("Grammar")
             .Exit
             .EndGrammar();
         yacc.Grammar("Grammar")
             .Enter
             .Gosub("NoWhere")
             .Exit
             .EndGrammar();
         try
         {
            var parser = yacc.CreateParser();
         }
         catch (GrammarBuildingException e)
         {
            Assert.AreEqual("Not all subgrammars are defined", e.Message);
            Assert.IsTrue(e.HasUndefinedSubgrammars);
            exceptionFound = true;
         }

         Assert.IsTrue(exceptionFound);
      }

      [TestMethod]
      public void BranchTest_CreateABranch_NoException()
      {
         var yacc = new Yallocc<DummyContext, Token>(new LexSharpCreator<Token>());
         yacc.DefineTokens()
         .AddTokenPattern(@"a", Token.A)
         .AddTokenPattern(@"b", Token.B)
         .AddTokenPattern(@"b", Token.C)
         .End();

         var branch = yacc.Branch.Label("BranchStart").Token(Token.A);

         Assert.IsTrue(branch is BranchBuilder<DummyContext, Token>);
         var branchBuilder = (branch as BranchBuilder<DummyContext, Token>);
         var grammarBuilder = branchBuilder.GrammarBuilder;
         Assert.AreEqual(grammarBuilder.Start.Name, "BranchStart");
      }

      [TestMethod]
      public void IsCompletTest_NotAnEnumTypeAndCompletenessChecked_NotAnEnumTypeException()
      {
         var yacc = new Yallocc<DummyContext, int>(new LexSharpCreator<int>());
         yacc.DefineTokens()
         .AddTokenPattern(@"one", 1)
         .AddTokenPattern(@"Two", 2)
         .AddTokenPattern(@"Three", 3)
         .End();

         bool exeptionThrown = false;
         try
         {
            var parser = yacc.CreateParser();
         }
         catch (TokenIsNotAnEnumTypeException)
         {
            exeptionThrown = true;
         }
         Assert.IsTrue(exeptionThrown);
      }

      [TestMethod]
      public void IsCompletTest_NotAnEnumTypeAndCompletenessNotChecked_NoException()
      {
         var yacc = new Yallocc<DummyContext, int>(new LexSharpCreator<int>()) { TokenCompletenessIsChecked = false };
         yacc.DefineTokens()
         .AddTokenPattern(@"one", 1)
         .AddTokenPattern(@"Two", 2)
         .AddTokenPattern(@"Three", 3)
         .End();
         yacc.MasterGrammar("MasterGrammar")
             .Enter
             .Token(1)
             .Token(2)
             .Token(3)
             .Exit
             .EndGrammar();

         bool exeptionThrown = false;
         try
         {
            var parser = yacc.CreateParser();
         }
         catch (TokenIsNotAnEnumTypeException)
         {
            exeptionThrown = true;
         }
         Assert.IsFalse(exeptionThrown);
      }

   }
}
