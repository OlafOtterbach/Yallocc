﻿using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class DimStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.Grammar("DimStatement")
             .Enter                           .Action(() => stb.Enter())
             .Token(TokenType.dim)            .Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
             .Token(TokenType.name)           .Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Token(TokenType.open)
             .Label("ParamList")
             .Gosub("Expression").Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.comma)
                     .Goto("ParamList"),
                 yacc.Branch.Default
              )
             .Token(TokenType.close)
             .Exit                            .Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}