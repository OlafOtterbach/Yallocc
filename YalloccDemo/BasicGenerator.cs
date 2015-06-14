﻿using LexSharp;
using Yallocc;
using SyntaxTree;

namespace YalloccDemo
{
   public class BasicGenerator
   {
      public YParser<Tokens> CreateParser()
      {
         var yacc = new Yallocc<Tokens>();
         DefineTokens(yacc);
//         DefineGrammar(yacc,);
         var parser = yacc.CreateParser();
         return parser;
      }

      private void DefineTokens(Yallocc<Tokens> yacc)
      {
         yacc.AddToken(@"\+", Tokens.plus);
         yacc.AddToken(@"\-", Tokens.minus);
         yacc.AddToken(@"\*", Tokens.mult);
         yacc.AddToken(@"\/", Tokens.div);
         yacc.AddToken(@"=", Tokens.equal);
         yacc.AddToken(@"\>", Tokens.greater);
         yacc.AddToken(@"\<", Tokens.less);
         yacc.AddToken(@"\(", Tokens.open);
         yacc.AddToken(@"\)", Tokens.close);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)+(\.(0|1|2|3|4|5|6|7|8|9)+)?", Tokens.number);
         yacc.AddToken(@"\w", Tokens.name);
      }

      private void DefineGrammar(Yallocc<Tokens> yacc, SyntaxTreeBuilder ctx)
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
                 yacc.Branch.Token(Tokens.equal).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok))),
                 yacc.Branch.Token(Tokens.greater).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok))),
                 yacc.Branch.Token(Tokens.less).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok)))
              )
             .Exit.Action(() => ctx.Exit())
             .EndGrammar();

         yacc.Grammar("SimpleExpression")
             .Enter.Name("SimpleExpressionStart").Action(() => ctx.Enter())
             .Switch
              (
                yacc.Branch.Token(Tokens.plus).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok))),
                yacc.Branch.Token(Tokens.minus).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok))),
                yacc.Branch.Default
              )
             .Gosub("Term")
             .Switch
              (
                yacc.Branch
                    .Token(Tokens.plus).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok)))
                    .Goto("SimpleExpressionStart"),
                yacc.Branch
                    .Token(Tokens.minus).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok)))
                    .Goto("SimpleExpressionStart"),
                yacc.Branch.Default
              )
             .Exit.Action(() => ctx.Exit())
             .EndGrammar();
 
         yacc.Grammar("Term")
             .Enter.Name("TermStart").Action(() => ctx.Enter())
             .Gosub("Factor")
             .Switch
              (
                yacc.Branch
                    .Token(Tokens.mult).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok)))
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(Tokens.div).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok)))
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
                     .Token(Tokens.number).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok))),
                 yacc.Branch
                     .Token(Tokens.name).Action((Token<Tokens> tok) => ctx.CreateParent(new TokenTreeNode<Tokens>(tok))),
                 yacc.Branch
                     .Token(Tokens.open)
                     .Gosub("Expression")
                     .Token(Tokens.close)
              )
             .Exit.Action(() => ctx.Exit())
             .EndGrammar();
      }
   }
}
