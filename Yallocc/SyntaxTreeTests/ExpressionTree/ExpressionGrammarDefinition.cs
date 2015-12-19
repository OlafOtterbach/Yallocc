using SyntaxTree;
using Yallocc;
using Yallocc.Tokenizer;

namespace SyntaxTreeTest.ExpressionTree
{
   public class ExpressionGrammarDefinition : ITokenAndGrammarDefinition<ExpressionTokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, ExpressionTokenType> yacc)
      {
         // Expression
         //                                               |---------[Relation]------>----|
         //                                               |                              |
         //                                               |-----[SimpleExpression]-->----|
         //                                               |                             \|/
         // --"ExpressionStart"-->--[SimpleExpression]----------------------------------------->
         //
         yacc.MasterGrammar("Expression")
             .Enter.Name("ExpressionStart").Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Gosub("SimpleExpression").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Gosub("Relation").Action((SyntaxTreeBuilder stb) => stb.CapInnerNodeToParent())
                     .Gosub("SimpleExpression").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch.Default
              )
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();

         // Relation
         //                         |-------equal-->--|
         //                         |                 |
         //                         |------greater->--|
         //                         |                \|/
         // --"RelationStart"--->-----------less--->--------->
         //
         yacc.Grammar("Relation")
             .Enter.Name("RelationStart").Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Switch
              (
                 yacc.Branch.Token(ExpressionTokenType.equal).Action((SyntaxTreeBuilder stb, Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                 yacc.Branch.Token(ExpressionTokenType.greater).Action((SyntaxTreeBuilder stb, Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                 yacc.Branch.Token(ExpressionTokenType.less).Action((SyntaxTreeBuilder stb, Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
              )
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
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
             .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Label("SimpleExpressionStart")
             .Switch
              (
                yacc.Branch.Token(ExpressionTokenType.plus).Action((SyntaxTreeBuilder stb, Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                yacc.Branch.Token(ExpressionTokenType.minus).Action((SyntaxTreeBuilder stb, Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                yacc.Branch.Default
              )
             .Label("SimpleExpressionLoop")
             .Gosub("Term").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                yacc.Branch
                    .Token(ExpressionTokenType.plus).Action((SyntaxTreeBuilder stb, Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
                    .Goto("SimpleExpressionLoop"),
                yacc.Branch
                    .Token(ExpressionTokenType.minus).Action((SyntaxTreeBuilder stb, Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
                    .Goto("SimpleExpressionLoop"),
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
                    .Token(ExpressionTokenType.mult).Action((SyntaxTreeBuilder stb, Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(ExpressionTokenType.div).Action((SyntaxTreeBuilder stb, Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
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
                     .Token(ExpressionTokenType.number).Action((SyntaxTreeBuilder stb, Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                 yacc.Branch
                     .Token(ExpressionTokenType.open)
                     .Gosub("Expression").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
                     .Token(ExpressionTokenType.close)
              )
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}
