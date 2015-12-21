using Yallocc.SyntaxTree;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Yallocc.SyntaxTreeDemo
{
   public static class ExpressionTreeCreator
   {
      public static Func<double> CreateExpression(SyntaxTreeNode root)
      {
         var expression = CreateExpressionNode(root);
         Expression<Func<double>> lambda = Expression.Lambda<Func<double>>(expression);
         Func<double> function = lambda.Compile();
         return function;
      }

      private static Expression CreateExpressionNode(SyntaxTreeNode node)
      {
         var tokNode = node as TokenTreeNode<TokenType>;
         Expression result = null;
         if (node.Children.Count() == 0)
         {
            double value = double.Parse(tokNode.Token.Value, CultureInfo.InvariantCulture);
            result = Expression.Constant(value, typeof(double));
         }
         else if (node.Children.Count() == 1)
         {
            var child = CreateExpressionNode(node.Children.First());
            if(tokNode.Token.Type == TokenType.minus)
            {
               result = Expression.Negate(child);
            }
            else
            {
               result = child;
            }
         }
         else if (node.Children.Count() == 2)
         {
            var left = CreateExpressionNode(node.Children.First());
            var right = CreateExpressionNode(node.Children.Last());
            switch (tokNode.Token.Type)
            {
               case TokenType.plus:
                  result = Expression.Add(left, right);
                  break;
               case TokenType.minus:
                  result = Expression.Subtract(left, right);
                  break;
               case TokenType.mult:
                  result = Expression.Multiply(left, right);
                  break;
               case TokenType.div:
                  result = Expression.Divide(left, right);
                  break;
               default:
                  break;
            }
         }
         return result;
      }
   }
}
