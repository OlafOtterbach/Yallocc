using SyntaxTree;
using System.Linq;

namespace SyntaxTreeTest.ExpressionTree
{
   public static class ExpressionCalculator
   {
      public static ExpressionResult Calculate(SyntaxTreeNode root)
      {
         var left = root.Children.Any() ? Calculate(root.Children.First()) : new ExpressionResult();
         var right = root.Children.Any() ? Calculate(root.Children.Last()) : new ExpressionResult();
         var tokNode = root as TokenTreeNode<ExpressionTokenType>;
         switch (tokNode.Token.Type)
         {
            case ExpressionTokenType.equal:
               if(left.IsBoolean)
               {
                  return new ExpressionResult(left.BooleanValue == right.BooleanValue);
               }
               else
               {
                  return new ExpressionResult(left.DoubleValue == right.DoubleValue);
               }
            case ExpressionTokenType.less:
               return new ExpressionResult(left.DoubleValue < right.DoubleValue);
            case ExpressionTokenType.greater:
               return new ExpressionResult(left.DoubleValue > right.DoubleValue);
            case ExpressionTokenType.plus:
               if(root.Children.Count() == 2)
               {
                  return new ExpressionResult(left.DoubleValue + right.DoubleValue);
               }
               else
               {
                  return new ExpressionResult(left.DoubleValue);
               }
            case ExpressionTokenType.minus:
               if (root.Children.Count() == 2)
               {
                  return new ExpressionResult(left.DoubleValue - right.DoubleValue);
               }
               else
               {
                  return new ExpressionResult(-left.DoubleValue);
               }
            case ExpressionTokenType.mult:
               return new ExpressionResult(left.DoubleValue * right.DoubleValue);
            case ExpressionTokenType.div:
               return new ExpressionResult(left.DoubleValue / right.DoubleValue);
            case ExpressionTokenType.number:
               return new ExpressionResult(double.Parse(tokNode.Token.Value));
            default:
               return new ExpressionResult();
         }
      }
   }
}
