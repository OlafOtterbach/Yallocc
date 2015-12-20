using Yallocc.Tokenizer;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class LetStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      // DIM
      //                             --------(,)-------
      //                             |                |
      //                            \|/               |
      //                    --(()->-----[Expression]----())--
      //                    |                               |
      //                    |                              \|/
      // --(LET)-->--(name)-------------------------------------(=)--[Expression]---->
      //
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         yacc.Grammar("LetStatement")
             .Enter                                    .Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Token(TokenType.let_keyword)             .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
             .Token(TokenType.name)                    .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.open)            .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
                     .Label("ParamList")
                     .Gosub("Expression")              .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
                     .Switch
                      (
                         yacc.Branch
                             .Token(TokenType.comma)
                             .Goto("ParamList"),
                         yacc.Branch.Default
                      )
                     .Token(TokenType.close)           .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch.Default
              )
             .Token(TokenType.equal)                   .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Gosub("Expression")                      .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}