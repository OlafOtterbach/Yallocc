using SyntaxTree;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BasicDemo.Basic
{
   public class ExpressionCommandCreator
   {
      BasicEngine _engine;

      public ExpressionCommandCreator(BasicEngine engine)
      {
         _engine = engine;
      }

      public ExpressionCommand Create(SyntaxTreeNode node)
      {
         var postOrderExpression = new List<BasicEntity>();
         Traverse(node, postOrderExpression);

         var expressionCommand = new ExpressionCommand((node as TokenTreeNode<TokenType>).Token, postOrderExpression);

         return expressionCommand;
      }

      private void Traverse(SyntaxTreeNode node, List<BasicEntity> postOrderExpr)
      {
         if(node is TokenTreeNode<TokenType>)
         {
            var tokNode = node as TokenTreeNode<TokenType>;

            // Post traverse
            if (tokNode.Token.Type != TokenType.name)
            {
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
                  if (node.Children.Any())
                  {
                     var arrayCreator = new ArrayStatementCreator(_engine);
                     elem = arrayCreator.Create(node);
                  }
                  else
                  {
                     elem = new VariableProxy(_engine, tokNode.Token.Value);
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
               case TokenType.not_keyword:
                  elem = new BasicNot();
                  break;
               case TokenType.mod_keyword:
                  elem = new BasicModulo();
                  break;
               case TokenType.and_keyword:
                  elem = new BasicAnd();
                  break;
               case TokenType.or_keyword:
                  elem = new BasicOr();
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
               case TokenType.lessEqual:
                  elem = new BasicLessEqual();
                  break;
               case TokenType.greater:
                  elem = new BasicGreater();
                  break;
               case TokenType.greaterEqual:
                  elem = new BasicGreaterEqual();
                  break;
               default:
                  throw new BasicTypeMissmatchException("Wrong element for expression.");
            }
            postOrderExpr.Add(elem);
         }
         else
         {
            throw new BasicTypeMissmatchException("No expression.");
         }
      }
   }
}
