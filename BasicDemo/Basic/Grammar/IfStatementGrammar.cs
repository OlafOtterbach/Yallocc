using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class IfStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.Grammar("IfStatement")
             .Enter                                  .Action(() => stb.Enter())
             .Token(TokenType.if_keyword)            .Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
             .Gosub("Expression")                    .Action(() => stb.AdoptInnerNodes())
             .Token(TokenType.then_keyword)          .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Gosub("StatementSequence")             .Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.end_keyword)   .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok))),
                 yacc.Branch
                     .Token(TokenType.else_keyword)  .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
                     .Gosub("StatementSequence")     .Action(() => stb.AdoptInnerNodes())
                     .Token(TokenType.end_keyword)   .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
              )
             .Exit                                   .Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}