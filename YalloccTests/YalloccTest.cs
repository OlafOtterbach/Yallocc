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
      public void AddTokenAndCreateParserTest_AddABC_NoException()
      {
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"a", Token.A);
         yacc.AddToken(@"b", Token.B);
         yacc.AddToken(@"c", Token.C);

         yacc.MasterGrammar("Grammar").Begin.End();
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

         yacc.MasterGrammar("Grammar").Begin.End();
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

         yacc.Grammar("Grammar").Begin.End();
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

         yacc.Grammar("Grammar").Begin.End();
         yacc.MasterGrammar("MasterGrammar").Begin.End();
         var parser = yacc.CreateParser();

         Assert.IsTrue(parser.Parse("").Success);
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
             .Begin
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
             .Begin
          .Gosub("Grammar")
             .End();
         yacc.Grammar("Grammar")
             .Begin
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

   }
}
