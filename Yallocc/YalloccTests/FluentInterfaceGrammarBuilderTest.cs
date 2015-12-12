using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer;

namespace Yallocc
{
   [TestClass]
   public class FluentInterfaceGrammarBuilderTest
   {
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
          .Token(AbcTokenType.a_token).Action((Token<AbcTokenType> tok) => { }).Name("eins")
          .Token(AbcTokenType.b_token).Name("zwei").Action((Token<AbcTokenType> tok) => { })
          .Gosub("A1").Action(() => {})
          .Switch(b.Branch.Token(AbcTokenType.a_token).Action((Token<AbcTokenType> tok) => { }).Name("eins")
                          .Token(AbcTokenType.b_token).Name("zwei").Action((Token<AbcTokenType> tok) => { }),
                  b.Branch.Gosub("A2").Action(() => { }),
                  b.Branch.Default.Action(() => {}).Name("Default1"),
                  b.Branch.Default.Name("Default1").Action(() => { })
                 )
          .Exit
          .EndGrammar();

         Assert.IsTrue(true);
      }

      private GrammarBuilderInterface<AbcTokenType> CreateBuilder()
      {
         var grammarDictionary = new GrammarDictionary();
         var baseBuilder = new GrammarBuilder<AbcTokenType>(grammarDictionary);
         var builderInterface = new GrammarBuilderInterface<AbcTokenType>(baseBuilder);
         return builderInterface;
      }
   }
}