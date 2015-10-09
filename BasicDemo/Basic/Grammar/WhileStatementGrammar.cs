using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class WhileStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.Grammar("WhileStatement")
             .Enter.Action(() => stb.Enter())
             .Token(TokenType.while_keyword).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
             .Gosub("Expression").Action(() => stb.AdoptInnerNodes())
             .Token(TokenType.do_keyword).Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Gosub("StatementSequence").Action(() => stb.AdoptInnerNodes())
             .Token(TokenType.end_keyword).Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}