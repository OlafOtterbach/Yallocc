using System.Linq;
using SyntaxTree;
using LexSharp;
using System.Collections.Generic;

namespace BasicDemo.Basic
{
   public class ForCommandCreator : CommandCreator
   {
      private static int _counter = 0;

      public ForCommandCreator(BasicEngine engine) : base(engine)
      { }

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode) && ((node as TokenTreeNode).Token.Type == TokenType.for_keyword);
         return result;
      }

      public override void Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode);

         var children = node.Children.OfType<TokenTreeNode>().ToList();
         var name = children[0].Token.Value;
         var expressionCreator = new ExpressionCommandCreator(Engine);
         var letExpression = expressionCreator.Create(children[2]);
         var letCommand = new LetCommand(tokNode.Token, Engine, name, letExpression);
         Engine.Add(letCommand);

         var forLabelName = "ForLabel" + (_counter++).ToString();
         var forLabelCommand = new LabelCommand(tokNode.Token, Engine, forLabelName);
         Engine.Add(forLabelCommand);
         var afterForLabelName = "ForLabel" + (_counter++).ToString();
         var afterForLabelCommand = new LabelCommand(tokNode.Token, Engine, afterForLabelName);

         var nameNode = children[0];
         var greaterNode = new TokenTreeNode(new Token<TokenType>(TokenType.greater));
         var expressionNode = children[4];
         greaterNode.Children = new List<SyntaxTreeNode>() { nameNode, expressionNode };
         var greaterExpressionCreator = new ExpressionCommandCreator(Engine);
         var greaterExpression = greaterExpressionCreator.Create(greaterNode);
         var ifCommand = new IfCommand(tokNode.Token, Engine, afterForLabelName, greaterExpression);
         Engine.Add(ifCommand);

         var nodes = children.Skip(5)
                             .TakeWhile(x => x.Token.Type != TokenType.end_keyword)
                             .ToList();
         var statementCreator = new StatementSequenceCreator(Engine);
         statementCreator.Create(nodes);

         var addNode = new TokenTreeNode(new Token<TokenType>(TokenType.plus));
         var stepNode = new TokenTreeNode(new Token<TokenType>("1", TokenType.integer, 0, 1));
         addNode.Children = new List<SyntaxTreeNode>() { nameNode, stepNode };
         var addExpressionCreator = new ExpressionCommandCreator(Engine);
         var addExpression = greaterExpressionCreator.Create(addNode);
         var addCommand = new LetCommand(tokNode.Token, Engine, name, addExpression);
         Engine.Add(addCommand);

         var gotoCommand = new GotoCommand(nodes.Any() ? nodes.Last().Token : tokNode.Token, Engine, forLabelName);
         Engine.Add(gotoCommand);

         Engine.Add(afterForLabelCommand);
      }
   }
}