using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Grammar
{
   public class AssignmentGrammar
   {
      public void DefineGrammar(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         //yacc.Grammar("Assignment")
         //    .Switch
         //     (
         //        yacc.Branch
         //            .Token(TokenType.let),
         //        yacc.Branch.Default
         //     )
         //    .Token(TokeType.name),
         //    .Token(TokeType.equal),
             
         //    .Enter.Name("ExpressionStart").Action(() => stb.Enter())
         //    .Gosub("SimpleExpression")
         //    .Switch
         //     (
         //        yacc.Branch
         //            .Gosub("Relation").Action(() => stb.CreateParent(stb.GetLastChild()))
         //            .Gosub("SimpleExpression"),
         //        yacc.Branch.Default
         //     )
         //    .Exit.Action(() => stb.Exit())
         //    .EndGrammar();
      }
   }
}