﻿using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class LetStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.Grammar("LetStatement")
             .Enter                                    .Action(() => stb.Enter())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.let),
                 yacc.Branch.Default
              )
             .Lambda                                   .Action(() => stb.CreateParent(new TokenTreeNode(new Token<TokenType>(TokenType.let))))
             .Token(TokenType.name)                    .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.open)            .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
                     .Label("ParamList")
                     .Gosub("Expression").Action(() => stb.AdoptInnerNodes())
                     .Switch
                      (
                         yacc.Branch
                             .Token(TokenType.comma)
                             .Goto("ParamList"),
                         yacc.Branch.Default
                      )
                     .Token(TokenType.close)           .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok))),
                 yacc.Branch.Default
              )
             .Token(TokenType.equal)                   .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Gosub("Expression")                      .Action(() => stb.AdoptInnerNodes())
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}