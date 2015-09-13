using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class ExpressionGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         // Expression
         //                                               |---------[Relation]------>----|
         //                                               |                              |
         //                                               |-----[SimpleExpression]-->----|
         //                                               |                             \|/
         // --"ExpressionStart"-->--[SimpleExpression]----------------------------------------->
         //
         yacc.Grammar("Expression")
             .Enter.Name("ExpressionStart")       .Action(() => stb.Enter())
             .Gosub("SimpleExpression")           .Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Gosub("Relation")           .Action(() => stb.CapInnerNodeToParent())
                     .Gosub("SimpleExpression")   .Action(() => stb.AdoptInnerNodes()),
                 yacc.Branch.Default
              )
             .Exit                                .Action(() => stb.Exit())
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
                 yacc.Branch.Token(TokenType.equal).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok))),
                 yacc.Branch.Token(TokenType.greater).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok))),
                 yacc.Branch.Token(TokenType.less).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
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
                yacc.Branch.Token(TokenType.plus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok))),
                yacc.Branch.Token(TokenType.minus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok))),
                yacc.Branch.Default
              )
             .Label("SimpleExpressionLoop")
             .Gosub("Term").Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.plus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
                    .Goto("SimpleExpressionLoop"),
                yacc.Branch
                    .Token(TokenType.minus).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
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
             .Gosub("Factor").Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.mult).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(TokenType.div).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
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
                     .Token(TokenType.integer).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok))),
                 yacc.Branch
                     .Token(TokenType.real).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok))),
                 yacc.Branch
                     .Token(TokenType.open)
                     .Gosub("Expression").Action(() => stb.AdoptInnerNodes())
                     .Token(TokenType.close),
                 yacc.Branch
                     .Token(TokenType.name).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
                     .Switch
                      (
                         yacc.Branch
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
                             .Token(TokenType.close),
                         yacc.Branch.Default
                      )
               )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}
