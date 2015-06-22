using System.Linq;
using LexSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YalloccDemo;
using SyntaxTree;
using Yallocc;
using YalloccDemo.Grammar;

namespace YalloccDemoTest
{
   [TestClass]
   public class ExpresssionTests
   {
      [TestMethod]
      public void Test()
      {
      }

      private SyntaxTreeNode CreateExpressionTree()
      {
         return null;
      }

      private void Create()
      {
         var stb = new SyntaxTreeBuilder();
         var yacc = new Yallocc<TokenType>();
         var tokenDefinition = new TokenDefinition();
         tokenDefinition.DefineExpressionTokens(yacc);
         var grammar = new ExpressionGrammar();
         grammar.DefineGrammar(yacc, stb);

      }
   }
}