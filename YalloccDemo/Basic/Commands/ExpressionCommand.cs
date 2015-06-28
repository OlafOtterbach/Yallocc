using System.Linq;
using System.Collections.Generic;

namespace YalloccDemo.Basic
{
   public class ExpressionCommand
   {
      private List<BasicEntity> _postOrderExpression;

      public ExpressionCommand(IEnumerable<BasicEntity> expression)
      {
         _postOrderExpression = expression.ToList();
      }

      public BasicEntity Execute()
      {
         var stack = new Stack<BasicEntity>();
         foreach(var entity in _postOrderExpression)
         {
            if(entity.IsVariable)
            {
               stack.Push(entity);
            }
            else if(entity.IsUnaryOperator)
            {
               stack.Push((entity as BasicUnaryOperator).Execute(stack.Pop()));
            }
            else
            {
               var right = stack.Pop();
               var left = stack.Pop();
               stack.Push((entity as BasicBinaryOperator).Execute(left, right));
            }
         }
         return stack.Pop();
      }
   }
}
