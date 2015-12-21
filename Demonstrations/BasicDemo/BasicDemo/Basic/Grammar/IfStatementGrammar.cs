using Yallocc.Tokenizer;
using Yallocc.SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class IfStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         // IF THEN ELSE END
         //
         //                                                         ---(ELSE)->-[StatementSequence]---
         //                                                         |                               \|/
         // --(IF)->-[Expression]->-(THEN)->-[StatementSequence]->----------------------------------------(END)-->
         //
         yacc.Grammar("IfStatement")
             .Enter                                  .Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Token(TokenType.if_keyword)            .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
             .Gosub("Expression")                    .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Token(TokenType.then_keyword)          .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Gosub("StatementSequence")             .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.else_keyword)  .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
                     .Gosub("StatementSequence")     .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch.Default
              )
             .Token(TokenType.end_keyword)           .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Exit                                   .Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}