using SyntaxTree;
using Yallocc;
using Yallocc.Tokenizer;

namespace SyntaxTreeDemo
{
   public class GrammarDefinition : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder,TokenType> yacc)
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
             .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Label("ExpressionStart")
             .Switch
              (
                yacc.Branch.Token(TokenType.plus).Action((SyntaxTreeBuilder stb, Token < TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                yacc.Branch.Token(TokenType.minus).Action((SyntaxTreeBuilder stb, Token < TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                yacc.Branch.Default
              )
             .Label("ExpressionLoop")
             .Gosub("Term").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.plus).Action((SyntaxTreeBuilder stb, Token < TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("ExpressionLoop"),
                yacc.Branch
                    .Token(TokenType.minus).Action((SyntaxTreeBuilder stb, Token < TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("ExpressionLoop"),
                yacc.Branch.Default
              )
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();

         // Term
         //           |------------mult---------|
         //           |                         |
         //           |------------div----------|
         //          \|/                        |               
         // -----"TermStart"-->--[Factor]-------------->
         //
         yacc.Grammar("Term")
             .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Label("TermStart")
             .Gosub("Factor").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.mult).Action((SyntaxTreeBuilder stb, Token < TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(TokenType.div).Action((SyntaxTreeBuilder stb, Token < TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("TermStart"),
                yacc.Branch.Default
              )
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();

         // Factor
         //                                             
         //                      |---------number------->---|
         //                      |                          |
         // --"FactorStart"-->-------(-[Expression]-)--->- \|/----->
         //
         yacc.Grammar("Factor")
             .Enter.Name("FactorStart").Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.integer).Action((SyntaxTreeBuilder stb, Token < TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch
                     .Token(TokenType.real).Action((SyntaxTreeBuilder stb, Token < TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch
                     .Token(TokenType.open)
                     .Gosub("Expression").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
                     .Token(TokenType.close)
              )
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}
