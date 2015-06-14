﻿using LexSharp;
using Yallocc;
using SyntaxTree;

namespace SyntaxTreeTest
{
   public class ExpressionGrammarGenerator
   {
      public YParser<ExpressionTokens> CreateParser(SyntaxTreeBuilder ctx)
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

      private void DefineGrammar(Yallocc<ExpressionTokens> yacc, SyntaxTreeBuilder ctx)
      {
         yacc.MasterGrammar("Expression")
             .Enter.Name("ExpressionStart").Action(() => ctx.Enter())
             .Gosub("SimpleExpression")
             .Switch
              (
                 yacc.Branch
                     .Gosub("Relation").Action(() => ctx.CreateParent(ctx.GetLastChild()))
                     .Gosub("SimpleExpression"),
                 yacc.Branch.Default
              )
             .Exit.Action(() => ctx.Exit())
             .EndGrammar();

         yacc.Grammar("Relation")
             .Enter.Name("RelationStart").Action(() => ctx.Enter())
             .Switch
              (
                 yacc.Branch.Token(ExpressionTokens.equal).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok))),
                 yacc.Branch.Token(ExpressionTokens.greater).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok))),
                 yacc.Branch.Token(ExpressionTokens.less).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok)))
              )
             .Exit.Action(() => ctx.Exit())
             .EndGrammar();

         yacc.Grammar("SimpleExpression")
             .Enter.Action(() => ctx.Enter())
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
             .Exit.Action(() => ctx.Exit())
             .EndGrammar();

         yacc.Grammar("Term")
             .Enter.Action(() => ctx.Enter())
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
             .Exit.Action(() => ctx.Exit())
             .EndGrammar();

         yacc.Grammar("Factor")
             .Enter.Name("FactorStart").Action(() => ctx.Enter())
             .Switch
              (
                 yacc.Branch
                     .Token(ExpressionTokens.number).Action((Token<ExpressionTokens> tok) => ctx.CreateParent(new TokenTreeNode<ExpressionTokens>(tok))),
                 yacc.Branch
                     .Token(ExpressionTokens.open)
                     .Gosub("Expression")
                     .Token(ExpressionTokens.close)
              )
             .Exit.Action(() => ctx.Exit())
             .EndGrammar();
      }
   }
}