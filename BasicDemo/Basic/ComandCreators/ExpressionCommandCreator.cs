using SyntaxTree;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BasicDemo.Basic
{
   public class ExpressionCommandCreator
   {
      private BasicEngine _engine;

      public ExpressionCommandCreator(BasicEngine engine)
      {
         _engine = engine;
      }

      public ExpressionCommand Create(SyntaxTreeNode node)
      {
         var postOrderExpression = new List<BasicEntity>();
         Traverse(node, postOrderExpression);

         var expressionCommand = new ExpressionCommand((node as TokenTreeNode).Token, postOrderExpression);

         return expressionCommand;
      }

      private void Traverse(SyntaxTreeNode node, List<BasicEntity> postOrderExpr)
      {
         if(node is TokenTreeNode)
         {
            var tokNode = node as TokenTreeNode;

            // Post traverse
            if (tokNode.Token.Type != TokenType.name)
            {
               // Search only if not array variable node
               node.Children.Reverse().ToList().ForEach(child => Traverse(child, postOrderExpr));
            }

            // Concerning for node
            BasicEntity elem = null;
            switch(tokNode.Token.Type)
            {
               case TokenType.integer:
                  elem = new BasicInteger(int.Parse(tokNode.Token.Value));
                  break;
               case TokenType.real:
                  elem = new BasicReal(double.Parse(tokNode.Token.Value, CultureInfo.InvariantCulture));
                  break;
               case TokenType.text:
                  elem = new BasicString(tokNode.Token.Value);
                  break;
               case TokenType.name:
                  elem = _engine.GetVariable(tokNode.Token.Value);
                  if(elem is BasicArray)
                  {
                     var arrayCreator = new ArrayStatementCreator(_engine);
                     elem = arrayCreator.Create(node);
                  }
                  break;
               case TokenType.plus:
                  if (tokNode.Children.Count() == 1)
                  {
                     elem = new BasicAdditionSign();
                  }
                  else
                  {
                     elem = new BasicAddition();
                  }
                  break;
               case TokenType.minus:
                  if (tokNode.Children.Count() == 1)
                  {
                     elem = new BasicNegation();
                  }
                  else
                  {
                     elem = new BasicSubtraction();
                  }
                  break;
               case TokenType.mult:
                  elem = new BasicMultiplication();
                  break;
               case TokenType.div:
                  elem = new BasicDivision();
                  break;
               case TokenType.equal:
                  elem = new BasicEquals();
                  break;
               case TokenType.less:
                  elem = new BasicLess();
                  break;
               case TokenType.greater:
                  elem = new BasicGreater();
                  break;
               default:
                  // Throw unknown type exception
                  break;
            }
            postOrderExpr.Add(elem);
         }
         else
         {
            // Throw ExpressionMismatchError
         }
      }
   }
}
