using SyntaxTree;
using System.Linq;

namespace BasicDemo.Basic
{
   public class ArrayStatementCreator
   {
      BasicEngine _engine;

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
               if (tokNode.Children.Any())
               {
                  var creator = new ExpressionCommandCreator(_engine);
                  var expressions = tokNode.Children.Select(child => creator.Create(child)).ToArray();
                  accessor = new BasicArrayElementAccessor(_engine, tokNode.Token.Value, expressions);
               }
               else
               {
                  // Throw Array without parameters
               }
            }
            else
            {
               throw new BasicTypeMissmatchException("No arry expression name.");
            }
         }
         else
         {
            throw new BasicTypeMissmatchException("No array expression.");
         }
         return accessor;
      }
   }
}
