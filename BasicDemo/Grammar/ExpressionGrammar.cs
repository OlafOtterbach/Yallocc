using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Grammar
{
   public class ExpressionGrammar
   {
      public void DefineGrammar(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         // Expression
         //                                               |---------[Relation]------>----|
         //                                               |                              |
         //                                               |-----[SimpleExpression]-->----|
         //                                               |                             \|/
         // --"ExpressionStart"-->--[SimpleExpression]----------------------------------------->
         //
         yacc.Grammar("Expression")
             .Enter.Name("ExpressionStart").Action(() => stb.Enter())
             .Gosub("SimpleExpression")
             .Switch
              (
                 yacc.Branch
                     .Gosub("Relation").Action(() => stb.CreateParent(stb.GetLastChild()))
                     .Gosub("SimpleExpression"),
                 yacc.Branch.Default
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();

         // Relation
         //                         |-------equal-->--|
         //                         |                 |
         //                         |------greater->--|
         //                         |                \|/
         // --"RelationStart"--->-----------less--->--------->
         //
         yacc.Grammar("Relation")
             .Enter.Name("RelationStart").Action(() => stb.Enter())
             .Switch
              (
                 yacc.Branch.Token(TokenType.equal).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch.Token(TokenType.greater).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch.Token(TokenType.less).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();

         // SimpleExpression
         //
         //                            |--plus---|            |-------------------plus----|
         //                            |         |            |                           |
         //                            |--minus--|            |-------------------minus---|
         //                            |        \|/          \|/                          |
         // --"SimpleExpressionStart"----------------"SimpleExpreesionLoop"------[Term]---------------->
         //
         yacc.Grammar("SimpleExpression")
             .Enter.Action(() => stb.Enter())
             .Label("SimpleExpressionStart")
             .Switch
              (
                yacc.Branch.Token(TokenType.plus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                yacc.Branch.Token(TokenType.minus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                yacc.Branch.Default
              )
             .Label("SimpleExpressionLoop")
             .Gosub("Term")
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.plus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("SimpleExpressionLoop"),
                yacc.Branch
                    .Token(TokenType.minus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("SimpleExpressionLoop"),
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
             .Gosub("Factor")
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
                     .Token(TokenType.number).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch
                     .Token(TokenType.open)
                     .Gosub("Expression")
                     .Token(TokenType.close)
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}
