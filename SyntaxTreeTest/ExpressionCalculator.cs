using System.Linq;
using SyntaxTree;

namespace SyntaxTreeTest
{
   public static class ExpressionCalculator
   {
      public static ExpressionResult Calculate(SyntaxTreeNode root)
      {
         return new ExpressionResult(CalculateDouble(root)); 
      }

      private static double CalculateDouble(SyntaxTreeNode root)
      {
         if(root.Children.Count() >= 2)
         {
            var left = CalculateDouble(root.Children.First());
            var right = CalculateDouble(root.Children.Last());
            var opTok = root as TokenTreeNode<ExpressionTokens>;
            if(opTok.Token.Type == ExpressionTokens.plus)
            {
               return left + right;
            }
            else if (opTok.Token.Type == ExpressionTokens.minus)
            {
               return left - right;
            }
            else if (opTok.Token.Type == ExpressionTokens.mult)
            {
               return left * right;
            }
            else // (opTok.Token.Type == ExpressionTokens.div)
            {
               return left / right;
            }
         }
         else if (root.Children.Count() == 1)
         {
            var opTok = root as TokenTreeNode<ExpressionTokens>;
            if (opTok.Token.Type == ExpressionTokens.plus)
            {
               return CalculateDouble(root.Children.First());
            }
            else // (opTok.Token.Type == ExpressionTokens.minus)
            {
               return -CalculateDouble(root.Children.First());
            }
         }
         else // (root.Children.Count() == 0)
         {
            var opTok = root as TokenTreeNode<ExpressionTokens>;
            var numText = opTok.Token.Value;
            var val = int.Parse(numText);
            return val;
         }
      }
   }
}
