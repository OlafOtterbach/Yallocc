using LexSharp;
using SyntaxTree;
using Yallocc;

namespace SyntaxTreeTest.ExpressionTree
{
   public class ExpressionGrammarDefinition : ITokenAndGrammarDefinition<ExpressionTokenType>
   {
      public void Define(Yallocc<ExpressionTokenType> yacc, SyntaxTreeBuilder stb)
      {
         // Expression
         //                                               |---------[Relation]------>----|
         //                                               |                              |
         //                                               |-----[SimpleExpression]-->----|
         //                                               |                             \|/
         // --"ExpressionStart"-->--[SimpleExpression]----------------------------------------->
         //
         yacc.MasterGrammar("Expression")
             .Enter.Name("ExpressionStart").Action(() => stb.Enter())
             .Gosub("SimpleExpression").Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Gosub("Relation").Action(() => stb.CapInnerNodeToParent())
                     .Gosub("SimpleExpression").Action(() => stb.AdoptInnerNodes()),
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
                 yacc.Branch.Token(ExpressionTokenType.equal).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                 yacc.Branch.Token(ExpressionTokenType.greater).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                 yacc.Branch.Token(ExpressionTokenType.less).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
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
                yacc.Branch.Token(ExpressionTokenType.plus).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                yacc.Branch.Token(ExpressionTokenType.minus).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                yacc.Branch.Default
              )
             .Label("SimpleExpressionLoop")
             .Gosub("Term").Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                yacc.Branch
                    .Token(ExpressionTokenType.plus).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
                    .Goto("SimpleExpressionLoop"),
                yacc.Branch
                    .Token(ExpressionTokenType.minus).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
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
                    .Token(ExpressionTokenType.mult).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(ExpressionTokenType.div).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
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
                     .Token(ExpressionTokenType.number).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                 yacc.Branch
                     .Token(ExpressionTokenType.open)
                     .Gosub("Expression").Action(() => stb.AdoptInnerNodes())
                     .Token(ExpressionTokenType.close)
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}
