using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class LetGrammar
   {
      public void DefineGrammar(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.Grammar("LetStatement")
             .Enter                                    .Action(() => stb.Enter())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.let),
                 yacc.Branch.Default
              )
             .Token(TokenType.name)                    .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.open_clamp)      .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
                     .Label("ParamList")
                     .Gosub("Expression")
                     .Switch
                      (
                         yacc.Branch
                             .Token(TokenType.comma)   .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
                             .Goto("ParamList"),
                         yacc.Branch.Default
                      )
                     .Token(TokenType.close_clamp)     .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok))),
                 yacc.Branch.Default
              )
             .Token(TokenType.equal)                   .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Gosub("Expression")
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}