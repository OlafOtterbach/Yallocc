using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class GotoStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.Grammar("GotoStatement")
             .Enter.Action(() => stb.Enter())
             .Token(TokenType.goto_keyword).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
             .Token(TokenType.name).Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}
