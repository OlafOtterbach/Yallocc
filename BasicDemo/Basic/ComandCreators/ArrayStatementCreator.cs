using System.Linq;
using SyntaxTree;

namespace BasicDemo.Basic
{
   public class ArrayStatementCreator
   {
      private BasicEngine _engine;

      public ArrayStatementCreator(BasicEngine engine)
      {
         _engine = engine;
      }

      public BasicArrayElementAccessor Create(SyntaxTreeNode node)
      {
         BasicArrayElementAccessor accessor = null;
         if (node is TokenTreeNode)
         {
            var tokNode = node as TokenTreeNode;
            if (tokNode.Token.Type == TokenType.name)
            {
               var elem = _engine.GetVariable(tokNode.Token.Value);
               if (elem is BasicArray)
               {
                  var basicArray = elem as BasicArray;
                  if (tokNode.Children.Any())
                  {
                     accessor = new BasicArrayElementAccessor(basicArray);
                     var creator = new ExpressionCommandCreator(_engine);
                     tokNode.Children.Select(child => creator.Create(child)).ToList().ForEach(expr => accessor.Add(expr));
                  }
                  else
                  {
                     // Throw Array without parameters
                  }
               }
            }
            else
            {
               // Throw ExpressionMismatchError
            }
         }
         else
         {
            // Throw ExpressionMismatchError
         }
         return accessor;
      }
   }
}
