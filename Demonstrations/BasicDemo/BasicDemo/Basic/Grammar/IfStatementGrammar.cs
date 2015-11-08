using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class IfStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         // IF THEN ELSE END
         //
         //                                                         ---(ELSE)->-[StatementSequence]---
         //                                                         |                               \|/
         // --(IF)->-[Expression]->-(THEN)->-[StatementSequence]->----------------------------------------(END)-->
         //
         yacc.Grammar("IfStatement")
             .Enter                                  .Action(() => stb.Enter())
             .Token(TokenType.if_keyword)            .Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
             .Gosub("Expression")                    .Action(() => stb.AdoptInnerNodes())
             .Token(TokenType.then_keyword)          .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Gosub("StatementSequence")             .Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.else_keyword)  .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
                     .Gosub("StatementSequence")     .Action(() => stb.AdoptInnerNodes()),
                 yacc.Branch.Default
              )
             .Token(TokenType.end_keyword)           .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Exit                                   .Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}