using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LexSharp;

namespace ParserLib
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
         var b = CreateBuilder();
         var grammar = b.CreateGrammar()
          .Begin
          .Token(AbcTokenType.a_token)
          .Token(AbcTokenType.b_token)
          .End;

         Assert.IsTrue(Parser("ab", grammar));
      }

      [TestMethod]
      public void GrammarTest_WithBranchAndGotoLoop_ParsingTextCorrect()
      {
         var b =  CreateBuilder();
         var grammar= b.CreateGrammar()
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
          .End;

         Assert.IsTrue(Parser("abbbca", grammar));
      }

      [TestMethod]
      public void GrammarTest_WithRecursion_ParsingTextCorrect()
      {
         var b = CreateBuilder();

         var container = b.CreateGrammar();

         var grammar = b.CreateGrammar()
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
          .End;

         Assert.IsTrue(Parser("abbbca", grammar));
      }

      private bool Parser(string text, Transition grammar)
      {
         var lex = CreateAbcLex();
         var parser = new Parser<AbcTokenType>();

         var sequence = lex.Scan(text);
         var result = parser.ParseTokens(grammar, sequence);

         return result;
      }

      private BuilderInterface<AbcTokenType> CreateBuilder()
      {
         var baseBuilder = new GrammarBuilder<AbcTokenType>();
         var builderInterface = new BuilderInterface<AbcTokenType>(baseBuilder);
         return builderInterface;
      }

      private Lex<AbcTokenType> CreateAbcLex()
      {
         var lex = new Lex<AbcTokenType>();
         lex.Register(@"a", AbcTokenType.a_token);
         lex.Register(@"b", AbcTokenType.b_token);
         lex.Register(@"c", AbcTokenType.c_token);
         return lex;
      }
   }
}
