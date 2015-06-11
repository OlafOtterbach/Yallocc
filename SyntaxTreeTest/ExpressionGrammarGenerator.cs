using LexSharp;
using Yallocc;
using YallocSyntaxTree;

namespace YalloccSyntaxTreeTest
{
   public class ExpressionGrammarGenerator
   {
      public YParser<ExpressionTokens> CreateParser(SyntaxTree ctx)
      {
         var yacc = new Yallocc<ExpressionTokens>();
         DefineExpressionTokens(yacc);
         DefineGrammar(yacc,ctx);
         var parser = yacc.CreateParser();
         return parser;
      }

      private void DefineExpressionTokens(Yallocc<ExpressionTokens> yacc)
      {
         yacc.AddToken(@"\+", ExpressionTokens.plus);
         yacc.AddToken(@"\-", ExpressionTokens.minus);
         yacc.AddToken(@"\*", ExpressionTokens.mult);
         yacc.AddToken(@"\/", ExpressionTokens.div);
         yacc.AddToken(@"=", ExpressionTokens.equal);
         yacc.AddToken(@"\>", ExpressionTokens.greater);
         yacc.AddToken(@"\<", ExpressionTokens.less);
         yacc.AddToken(@"\(", ExpressionTokens.open);
         yacc.AddToken(@"\)", ExpressionTokens.close);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)+(\.(0|1|2|3|4|5|6|7|8|9)+)?", ExpressionTokens.number);
      }

      private void DefineGrammar(Yallocc<ExpressionTokens> yacc, SyntaxTree ctx)
      {
         yacc.MasterGrammar("Expression")
             .Begin
             .Label("ExpressionStart").Action(() => ctx.Enter(1))
             .Gosub("SimpleExpression")
             .Switch
              (
                 yacc.Branch
                     .Gosub("Relation").Action(() => ctx.CreateParent(ctx.GetLastChild()))
                     .Gosub("SimpleExpression"),
                 yacc.Branch.Default
              )
             .Lambda(() => ctx.Exit(1))
             .End();

         yacc.Grammar("Relation")
             .Begin
             .Label("RelationStart").Action(() => ctx.Enter(77))
             .Switch
              (
                 yacc.Branch.Token(ExpressionTokens.equal).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok))),
                 yacc.Branch.Token(ExpressionTokens.greater).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok))),
                 yacc.Branch.Token(ExpressionTokens.less).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok)))
              )
             .Lambda(() => ctx.Exit(77))
             .End();

         yacc.Grammar("SimpleExpression")
             .Begin
             .Lambda(() => ctx.Enter(2))
             .Label("SimpleExpressionStart")
             .Switch
              (
                yacc.Branch.Token(ExpressionTokens.plus).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok))),
                yacc.Branch.Token(ExpressionTokens.minus).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok))),
                yacc.Branch.Default
              )
             .Gosub("Term")
             .Switch
              (
                yacc.Branch
                    .Token(ExpressionTokens.plus).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok)))
                    .Goto("SimpleExpressionStart"),
                yacc.Branch
                    .Token(ExpressionTokens.minus).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok)))
                    .Goto("SimpleExpressionStart"),
                yacc.Branch.Default
              )
             .Lambda(() => ctx.Exit(2))
             .End();

         yacc.Grammar("Term")
             .Begin
             .Lambda(() => ctx.Enter(3))
             .Label("TermStart")
             .Gosub("Factor")
             .Switch
              (
                yacc.Branch
                    .Token(ExpressionTokens.mult).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok)))
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(ExpressionTokens.div).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok)))
                    .Goto("TermStart"),
                yacc.Branch.Default
              )
             .Lambda(() => ctx.Exit(3))
             .End();

         yacc.Grammar("Factor")
             .Begin
             .Label("FactorStart").Action(() => ctx.Enter(4))
             .Switch
              (
                 yacc.Branch
                     .Token(ExpressionTokens.number).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok))),
                 yacc.Branch
                     .Token(ExpressionTokens.open)
                     .Gosub("Expression")
                     .Token(ExpressionTokens.close)
              )
             .Lambda(() => ctx.Exit(4))
             .End();
      }
   }
}