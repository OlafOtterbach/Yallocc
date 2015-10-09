using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class ForStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.Grammar("ForStatement")
             .Enter.Action(() => stb.Enter())
             .Token(TokenType.for_keyword).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
             .Token(TokenType.name).Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Token(TokenType.equal).Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Gosub("Expression").Action(() => stb.AdoptInnerNodes())
             .Token(TokenType.to_keyword).Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Gosub("Expression").Action(() => stb.AdoptInnerNodes())
             .Token(TokenType.do_keyword).Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Gosub("StatementSequence").Action(() => stb.AdoptInnerNodes())
             .Token(TokenType.end_keyword).Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}