using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.SyntaxTree;
using Yallocc.SyntaxTreeTest.ExpressionTree;
using Yallocc.SyntaxTreeTest.SimpleGrammar;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc.SyntaxTreeTest
{
   [TestClass]
   public class SimpleGrammarTest
   {
      /// <summary>
      /// Tests that parsing gets the three tokens up to root node through grammar hierarchy.
      /// </summary>
      [TestMethod]
      public void ParseTest_WhenTextForThreeTokens_ThenRootWithThreeTokens()
      {
         var root = Parse("ABC");

         Assert.AreNotEqual(null, root);
      }

      private SyntaxTreeNode Parse(string text)
      {
         var generator
             = SyntaxTreeGeneratorCreator<TokenType>.RegisterDefinitions(new LeTokCreator<TokenType>())
                                                       .Register(new SimpleTokenDefinition())
                                                       .Register(new SimpleGrammarDefinition())
                                                       .Create();
         var res = generator.Parse(text);
         var node = res.Success ? res.Root : null;
         return node;
      }
   }
}