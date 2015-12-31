using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer;

namespace Yallocc
{
   [TestClass]
   public class FluentInterfaceGrammarBuilderTest
   {
      private class DummyContext {}

      private enum AbcTokenType
      {
         a_token,
         b_token,
         c_token,
      }

      [TestMethod]
      public void CompileTimeFluentInterfaceTest_AllPossibleBranches_AllwaysTrue()
      {
         var b = CreateBuilder();
         b.Grammar("Grammar")
          .Enter
          .Token(AbcTokenType.a_token).Action((DummyContext ctx, Token < AbcTokenType> tok) => { }).Name("eins")
          .Token(AbcTokenType.b_token).Name("zwei").Action((DummyContext ctx, Token < AbcTokenType> tok) => { })
          .Gosub("A1").Action((DummyContext ctx) => {})
          .Switch(b.Branch.Token(AbcTokenType.a_token).Action((DummyContext ctx, Token < AbcTokenType> tok) => { }).Name("eins")
                          .Token(AbcTokenType.b_token).Name("zwei").Action((DummyContext ctx, Token < AbcTokenType> tok) => { }),
                  b.Branch.Gosub("A2").Action((DummyContext ctx) => { }),
                  b.Branch.Default.Action((DummyContext ctx) => {}).Name("Default1"),
                  b.Branch.Default.Name("Default1").Action((DummyContext ctx) => { })
                 )
          .Exit
          .EndGrammar();

         Assert.IsTrue(true);
      }

      [TestMethod]
      public void CompileTimeFluentInterfaceTest_AnyTokenInMainAndBranch_AllwaysTrue()
      {
         var b = CreateBuilder();
         b.Grammar("Grammar")
          .Enter
          .Token(AbcTokenType.a_token).Action((DummyContext ctx, Token<AbcTokenType> tok) => { }).Name("eins")
          .AnyToken().Action((DummyContext ctx, Token<AbcTokenType> tok) => { }).Name("Any token")
          .Token(AbcTokenType.b_token).Name("zwei").Action((DummyContext ctx, Token<AbcTokenType> tok) => { })
          .AnyToken().Name("Any token").Action((DummyContext ctx, Token<AbcTokenType> tok) => { })
          .Switch(b.Branch.Token(AbcTokenType.a_token).Action((DummyContext ctx, Token<AbcTokenType> tok) => { }).Name("eins")
                          .Token(AbcTokenType.b_token).Name("zwei").Action((DummyContext ctx, Token<AbcTokenType> tok) => { }),
                  b.Branch.AnyToken().Action((DummyContext ctx, Token<AbcTokenType> tok) => { }).Name("Any token"),
                  b.Branch.AnyToken().Name("Any token").Action((DummyContext ctx, Token<AbcTokenType> tok) => { })
                 )
          .Exit
          .EndGrammar();
      }

      private GrammarBuilderInterface<DummyContext,AbcTokenType> CreateBuilder()
      {
         var grammarDictionary = new GrammarDictionary();
         var baseBuilder = new GrammarBuilder<DummyContext,AbcTokenType>(grammarDictionary);
         var builderInterface = new GrammarBuilderInterface<DummyContext,AbcTokenType>(baseBuilder);
         return builderInterface;
      }
   }
}