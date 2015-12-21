using System.Linq;
using Yallocc.SyntaxTree;

namespace BasicDemo.Basic
{
   public class WhileCommandCreator : CommandCreator
   {
      private static int _counter = 0;

      public WhileCommandCreator(BasicEngine engine) : base(engine)
      { }

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode<TokenType>) && ((node as TokenTreeNode<TokenType>).Token.Type == TokenType.while_keyword);
         return result;
      }

      public override void Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode<TokenType>);

         var whileLabelName = "WhileLabel" + (_counter++).ToString();
         var whileLabelCommand = new LabelCommand(tokNode.Token, Engine, whileLabelName);
         Engine.Add(whileLabelCommand);

         var children = node.Children.OfType<TokenTreeNode<TokenType>>().ToList();
         var afterWhileLabelName = "WhileLabel" + (_counter++).ToString();
         var expressionCreator = new ExpressionCommandCreator(Engine);
         var expression = expressionCreator.Create(children[0]);
         var ifCommand = new IfCommand(tokNode.Token, Engine, afterWhileLabelName, expression);
         Engine.Add(ifCommand);

         var nodes = children.Skip(2)
                                 .TakeWhile(x => (x.Token.Type != TokenType.end_keyword) && (x.Token.Type != TokenType.else_keyword))
                                 .ToList();
         var creator = new StatementSequenceCreator(Engine);
         creator.Create(nodes);

         var gotoCommand = new GotoCommand(nodes.Any() ? nodes.Last().Token : tokNode.Token, Engine, whileLabelName);
         Engine.Add(gotoCommand);

         var afterWhileNode = children.Where(x => x.Token.Type == TokenType.end_keyword)
                                      .First();
         var afterWhileLabelCommand = new LabelCommand(afterWhileNode.Token, Engine, afterWhileLabelName);
         Engine.Add(afterWhileLabelCommand);
      }
   }
}
