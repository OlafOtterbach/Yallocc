using LexSharp;
using SyntaxTree;
using Yallocc;

namespace SyntaxTreeTest.ExpressionTree
{
   public class ExpressionGrammarGenerator
   {
      public YParser<ExpressionTokenType> CreateParser(SyntaxTreeBuilder stb)
      {
         var yacc = new Yallocc<ExpressionTokenType>();
         DefineExpressionTokens(yacc);
         DefineGrammar(yacc,stb);
         var parser = yacc.CreateParser();
         return parser;
      }

      private void DefineExpressionTokens(Yallocc<ExpressionTokenType> yacc)
      {
         yacc.AddToken(@"\+", ExpressionTokenType.plus);
         yacc.AddToken(@"\-", ExpressionTokenType.minus);
         yacc.AddToken(@"\*", ExpressionTokenType.mult);
         yacc.AddToken(@"\/", ExpressionTokenType.div);
         yacc.AddToken(@"=", ExpressionTokenType.equal);
         yacc.AddToken(@"\>", ExpressionTokenType.greater);
         yacc.AddToken(@"\<", ExpressionTokenType.less);
         yacc.AddToken(@"\(", ExpressionTokenType.open);
         yacc.AddToken(@"\)", ExpressionTokenType.close);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)+(\.(0|1|2|3|4|5|6|7|8|9)+)?", ExpressionTokenType.number);
      }

      private void DefineGrammar(Yallocc<ExpressionTokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.MasterGrammar("Expression")
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

         yacc.Grammar("SimpleExpression")
             .Enter.Action(() => stb.Enter())
             .Label("SimpleExpressionStart")
             .Switch
              (
                yacc.Branch.Token(ExpressionTokenType.plus).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                yacc.Branch.Token(ExpressionTokenType.minus).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                yacc.Branch.Default
              )
             .Gosub("Term")
             .Switch
              (
                yacc.Branch
                    .Token(ExpressionTokenType.plus).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
                    .Goto("SimpleExpressionStart"),
                yacc.Branch
                    .Token(ExpressionTokenType.minus).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok)))
                    .Goto("SimpleExpressionStart"),
                yacc.Branch.Default
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();

         yacc.Grammar("Term")
             .Enter.Action(() => stb.Enter())
             .Label("TermStart")
             .Gosub("Factor")
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

         yacc.Grammar("Factor")
             .Enter.Name("FactorStart").Action(() => stb.Enter())
             .Switch
              (
                 yacc.Branch
                     .Token(ExpressionTokenType.number).Action((Token<ExpressionTokenType> tok) => stb.CreateParent(new TokenTreeNode<ExpressionTokenType>(tok))),
                 yacc.Branch
                     .Token(ExpressionTokenType.open)
                     .Gosub("Expression")
                     .Token(ExpressionTokenType.close)
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}