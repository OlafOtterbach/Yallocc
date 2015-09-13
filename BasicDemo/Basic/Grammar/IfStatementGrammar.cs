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
             .Token(TokenType.if_keyword)            .Action(() => stb.CreateParent(new TokenTreeNode(new Token<TokenType>(TokenType.if_keyword))))
             .Gosub("Expression")                    .Action(() => stb.AdoptInnerNodes())
             .Token(TokenType.then_keyword)          .Action(() => stb.CreateParent(new TokenTreeNode(new Token<TokenType>(TokenType.then_keyword))))
             .Gosub("StatementSequence")             .Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.end_keyword)   .Action(() => stb.CreateParent(new TokenTreeNode(new Token<TokenType>(TokenType.end_keyword)))),
                 yacc.Branch
                     .Token(TokenType.else_keyword)  .Action(() => stb.CreateParent(new TokenTreeNode(new Token<TokenType>(TokenType.else_keyword))))
                     .Gosub("StatementSequence")     .Action(() => stb.AdoptInnerNodes())
                     .Token(TokenType.end_keyword)   .Action(() => stb.CreateParent(new TokenTreeNode(new Token<TokenType>(TokenType.end_keyword))))
              )
             .Exit                                   .Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}