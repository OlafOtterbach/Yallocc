using LexSharp;
using Yallocc;

namespace YalloccDemo
{
   public class BasicGenerator
   {
      public YParser<Tokens> CreateParser()
      {
         var yacc = new Yallocc<Tokens>();
         DefineTokens(yacc);
         DefineGrammar(yacc);
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
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)+[.(0|1|2|3|4|5|6|7|8|9)+]", Tokens.number);
         yacc.AddToken(@"\w", Tokens.name);
      }

      private void DefineGrammar(Yallocc<Tokens> yacc)
      {
         yacc.MasterGrammar("Expression")
             .Begin
             .Gosub("SimpleExpression")
             .Switch
              (
                 yacc.Branch
                     .Gosub("Relation")
                     .Gosub("SimpleExpression"),
                 yacc.Branch.Default
              )
             .End();

         yacc.Grammar("Relation")
             .Begin
             .Switch
              (
                 yacc.Branch.Token(Tokens.equal),
                 yacc.Branch.Token(Tokens.greater),
                 yacc.Branch.Token(Tokens.less)
              )
              .End();

         yacc.Grammar("SimpleExpression")
             .Begin
             .Switch
              (
                yacc.Branch.Token(Tokens.plus),
                yacc.Branch.Token(Tokens.minus),
                yacc.Branch.Default
              )
             .Label("SimpleExpressionStart")
             .Gosub("Term")
             .Switch
              (
                yacc.Branch
                    .Token(Tokens.plus)
                    .Goto("SimpleExpressionStart"),
                yacc.Branch
                    .Token(Tokens.minus)
                    .Goto("SimpleExpressionStart"),
                yacc.Branch.Default
              )
             .End();

         yacc.Grammar("Term")
             .Begin
             .Label("TermStart")
             .Gosub("Factor")
             .Switch
              (
                yacc.Branch.Token(Tokens.mult).Goto("TermStart"),
                yacc.Branch.Token(Tokens.div).Goto("TermStart"),
                yacc.Branch.Default
              )
             .End();

         yacc.Grammar("Factor")
             .Begin
             .Switch
              (
                 yacc.Branch.Token(Tokens.number),
                 yacc.Branch.Token(Tokens.name),
                 yacc.Branch
                     .Token(Tokens.open)
                     .Gosub("Expression")
                     .Token(Tokens.close)
              )
             .End();
      }
   }
}
