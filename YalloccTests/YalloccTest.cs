using LexSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

      [TestMethod]
      public void SwitchTest()
      {
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);
         yacc.AddToken(@"c", Token.C);

         yacc.MasterGrammar("MasterGrammar")
             .Enter
             .Switch
              (
                 yacc.Branch.Token(Token.A),
                 yacc.Branch.Token(Token.B),
                 yacc.Branch.Token(Token.C)
              )
             .End();

         var parser = yacc.CreateParser();

         var res1 = parser.Parse("a");

         Assert.IsTrue(res1.Success);
      }

      [TestMethod]
      public void RecursionTest()
      {
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);
         yacc.AddToken(@"c", Token.C);

         yacc.MasterGrammar("MasterGrammar")
             .Enter
             .Label("MasterGrammar")
             .Token(Token.A)
             .Gosub("Grammar")
             .Token(Token.A)
             .End();

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
             .End();

         var parser = yacc.CreateParser();

         var res1 = parser.Parse("aba");

         Assert.IsTrue(res1.Success);
      }

      [TestMethod]
      public void AddTokenAndCreateParserTest_AddABC_NoException()
      {
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);
         yacc.AddToken(@"c", Token.C);

         yacc.MasterGrammar("Grammar").Enter.End();
         var parser = yacc.CreateParser();

         Assert.AreNotEqual(parser, null);
      }

      [TestMethod]
      public void AddTokenAndCreateParserTest_AddAB_NotCompleteException()
      {
         var exceptionFound = false;
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);

         yacc.MasterGrammar("Grammar").Enter.End();
         try
         {
            var parser = yacc.CreateParser();
         }
         catch(MissingTokenDefinitionException e)
         {
            Assert.AreEqual(e.Message, "Not all types of tokens are defined.");
            exceptionFound = true;
         }

         Assert.IsTrue(exceptionFound);
      }

      [TestMethod]
      public void GrammarAndCreateParserTest_NoMasterGrammar_NoMasterGrammarException()
      {
         var exceptionFound = false;
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);
         yacc.AddToken(@"b", Token.C);

         yacc.Grammar("Grammar").Enter.End();
         try
         {
            var parser = yacc.CreateParser();
         }
         catch (GrammarBuildingException e)
         {
            Assert.AreEqual(e.Message, "Master grammar is not defined.");
            Assert.IsTrue(e.MasterGrammarIsNotDefined);
            exceptionFound = true;
         }

         Assert.IsTrue(exceptionFound);
      }

      [TestMethod]
      public void GrammarAndCreateParserTest_GrammarAndMasterGrammar_NoException()
      {
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);
         yacc.AddToken(@"b", Token.C);

         yacc.Grammar("Grammar").Enter.End();
         yacc.MasterGrammar("MasterGrammar").Enter.End();
         var parser = yacc.CreateParser();

         Assert.IsTrue(parser.Parse("").Success);
      }

      [TestMethod]
      public void MasterGrammar_MasterGrammarDefinedTwice_MasterGrammarAlreadyDefinedException()
      {
         var exceptionFound = false;
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);
         yacc.AddToken(@"b", Token.C);

         try
         {
            yacc.MasterGrammar("MasterGrammar1").Enter.End();
            yacc.MasterGrammar("MasterGrammar2").Enter.End();
         }
         catch (GrammarBuildingException e)
         {
            Assert.AreEqual(e.Message, "Master grammar already defined.");
            Assert.IsTrue(e.MasterGrammarAlreadyDefined);
            exceptionFound = true;
         }

         Assert.IsTrue(exceptionFound);
      }

      [TestMethod]
      public void CreateParserTest_MasterGrammarWithWrongLinkToGrammar_HasUndefinedSubGrammarException()
      {
         var exceptionFound = false;
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);
         yacc.AddToken(@"b", Token.C);

         yacc.MasterGrammar("Grammar")
             .Enter
          .Gosub("NoWhere")
             .End();
         try
         {
            var parser = yacc.CreateParser();
         }
         catch (GrammarBuildingException e)
         {
            Assert.AreEqual(e.Message, "Not all subgrammars are defined");
            Assert.IsTrue(e.HasUndefinedSubgrammars);
            exceptionFound = true;
         }

         Assert.IsTrue(exceptionFound);
      }

      [TestMethod]
      public void CreateParserTest_MasterGrammarWithNestedGrammarWithWrongLinkToGrammar_HasUndefinedSubGrammarException()
      {
         var exceptionFound = false;
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);
         yacc.AddToken(@"b", Token.C);

         yacc.MasterGrammar("MasterGrammar")
             .Enter
          .Gosub("Grammar")
             .End();
         yacc.Grammar("Grammar")
             .Enter
          .Gosub("NoWhere")
             .End();
         try
         {
            var parser = yacc.CreateParser();
         }
         catch (GrammarBuildingException e)
         {
            Assert.AreEqual(e.Message, "Not all subgrammars are defined");
            Assert.IsTrue(e.HasUndefinedSubgrammars);
            exceptionFound = true;
         }

         Assert.IsTrue(exceptionFound);
      }

      [TestMethod]
      public void BranchTest_CreateABranch_NoException()
      {
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);
         yacc.AddToken(@"b", Token.C);

         var branch = yacc.Branch.Label("BranchStart").Token(Token.A);

         Assert.IsTrue(branch is BranchBuilder<Token>);
         var branchBuilder = (branch as BranchBuilder<Token>);
         var grammarBuilder = branchBuilder.GrammarBuilder;
         Assert.AreEqual(grammarBuilder.Start.Name, "BranchStart");
      }
   }
}
