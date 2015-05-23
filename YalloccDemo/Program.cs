using LexSharp;
using System.Linq;
using Yallocc;

namespace YalloccDemo
{
   public enum Token
   {
      plus,
      minus,
      mult,
      div,
      equal,
      greater,
      less,
      open,
      close,
      number,
      name
   }


   class Program
   {
      public void CreateGrammar()
      {
         var yacc = new Yallocc<Token>();
         yacc.AddToken(@"\+",Token.plus);
         yacc.AddToken(@"\-",Token.minus);
         yacc.AddToken(@"\*",Token.mult);
         yacc.AddToken(@"\/",Token.div);
         yacc.AddToken(@"=", Token.equal);
         yacc.AddToken(@"\>", Token.greater);
         yacc.AddToken(@"\<", Token.less);
         yacc.AddToken(@"\(", Token.open);
         yacc.AddToken(@"\)", Token.close);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)+[.(0|1|2|3|4|5|6|7|8|9)+]", Token.number);
         yacc.AddToken(@"\w", Token.name);

         var relation = yacc.Grammar("Relation")
            .Begin
            .Switch
             (
                yacc.Branch.Token(Token.equal),
                yacc.Branch.Token(Token.greater),
                yacc.Branch.Token(Token.less)
             )
            .End;

         //var term = yacc.CreateGrammar()
         //   .Begin
         //   .Label("TermStart")
         //   .Gosub(factor)
         //   .Switch
         //    (
         //      yacc.Branch.Token(Token.mult).Goto("TermStart"),
         //      yacc.Branch.Token(Token.div).Goto("TermStart"),
         //      yacc.Branch.Default
         //    )
         //   .End;

         var simpleExpression = yacc.Grammar("SimpleExpression")
            .Begin
            .Switch
             (
               yacc.Branch.Token(Token.plus),
               yacc.Branch.Token(Token.minus),
               yacc.Branch.Default
             )
            .Label("SimpleExpressionStart")
            .Gosub("Term")
            .Switch
             (
               yacc.Branch
                   .Token(Token.plus)
                   .Goto("SimpleExpressionStart"),
               yacc.Branch
                   .Token(Token.minus)
                   .Goto("SimpleExpressionStart"),
               yacc.Branch.Default
             )
            .End;

         var expression = yacc.Grammar("Expression")
            .Begin
            .Gosub("SimpleExpression")
            .Switch
             (
                yacc.Branch
                    .Gosub("Relation")
                    .Gosub("SimpleExpression"),
                yacc.Branch.Default
             )
            .End;

         var factor = yacc.Grammar("Factor")
            .Begin
            .Switch
             (
                yacc.Branch.Token(Token.number),
                yacc.Branch.Token(Token.name),
                yacc.Branch
                    .Token(Token.open)
                    .Gosub("Expression")
                    .Token(Token.close)
             )
            .End;

      }


      static void Main(string[] args)
      {

      }
   }
}
