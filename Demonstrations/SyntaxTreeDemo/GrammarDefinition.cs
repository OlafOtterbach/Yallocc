using LexSharp;
using SyntaxTree;
using Yallocc;

namespace SyntaxTreeDemo
{
   public class GrammarDefinition : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         // Expression
         //
         //                            |--plus---|            |-------------------plus----|
         //                            |         |            |                           |
         //                            |--minus--|            |-------------------minus---|
         //                            |        \|/          \|/                          |
         // --"ExpressionStart"----------------------"ExpreesionLoop"------[Term]---------------->
         //
         yacc.MasterGrammar("Expression")
             .Enter.Action(() => stb.Enter())
             .Label("ExpressionStart")
             .Switch
              (
                yacc.Branch.Token(TokenType.plus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                yacc.Branch.Token(TokenType.minus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                yacc.Branch.Default
              )
             .Label("ExpressionLoop")
             .Gosub("Term").Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.plus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("ExpressionLoop"),
                yacc.Branch
                    .Token(TokenType.minus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("ExpressionLoop"),
                yacc.Branch.Default
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();

         // Term
         //           |------------mult---------|
         //           |                         |
         //           |------------div----------|
         //          \|/                        |               
         // -----"TermStart"-->--[Factor]-------------->
         //
         yacc.Grammar("Term")
             .Enter.Action(() => stb.Enter())
             .Label("TermStart")
             .Gosub("Factor").Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.mult).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(TokenType.div).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("TermStart"),
                yacc.Branch.Default
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();

         // Factor
         //                                             
         //                      |---------number------->---|
         //                      |                          |
         // --"FactorStart"-->-------(-[Expression]-)--->- \|/----->
         //
         yacc.Grammar("Factor")
             .Enter.Name("FactorStart").Action(() => stb.Enter())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.integer).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch
                     .Token(TokenType.real).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch
                     .Token(TokenType.open)
                     .Gosub("Expression").Action(() => stb.AdoptInnerNodes())
                     .Token(TokenType.close)
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}
