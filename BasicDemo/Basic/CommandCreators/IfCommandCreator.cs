using System.Linq;
using SyntaxTree;

namespace BasicDemo.Basic
{
   public class IfCommandCreator : CommandCreator
   {
      private static int _counter = 0;

      public IfCommandCreator(BasicEngine engine) : base(engine)
      { }

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode) && ((node as TokenTreeNode).Token.Type == TokenType.if_keyword);
         return result;
      }

      public override void Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode);
         var children = node.Children.OfType<TokenTreeNode>().ToList();
         var thenLabelName = "IfLabel" + (_counter++).ToString();
         var expressionCreator = new ExpressionCommandCreator(Engine);
         var expression = expressionCreator.Create(children[0]);
         var ifCommand = new IfCommand(tokNode.Token, Engine, thenLabelName, expression);
         Engine.Add(ifCommand);

         var thenNodes = children.Skip(2)
                                 .TakeWhile(x => (x.Token.Type != TokenType.end_keyword) && (x.Token.Type != TokenType.else_keyword))
                                 .ToList();
         var thenCreator = new StatementSequenceCreator(Engine);
         thenCreator.Create(thenNodes);

         var afterThenNode = children.Select((n, i) => new { Node = n, Index = i })
                                     .Where(x => (x.Node.Token.Type == TokenType.end_keyword) || (x.Node.Token.Type == TokenType.else_keyword))
                                     .First();
         var labelCommand = new LabelCommand(afterThenNode.Node.Token, Engine, thenLabelName);

         if (afterThenNode.Node.Token.Type == TokenType.else_keyword)
         {
            var endLabelName = "IfLabel" + (_counter++).ToString();
            var gotoCommand = new GotoCommand(afterThenNode.Node.Token, Engine, endLabelName);
            Engine.Add(gotoCommand);

            Engine.Add(labelCommand);

            var elseNodes = children.Skip(afterThenNode.Index)
                                    .TakeWhile(x => x.Token.Type != TokenType.end_keyword)
                                    .ToList();
            var elseCreator = new StatementSequenceCreator(Engine);
            elseCreator.Create(elseNodes);

            var afterElseNode = children.Where(x => x.Token.Type == TokenType.end_keyword)
                                        .First();
            var endLabelCommand = new LabelCommand(afterElseNode.Token, Engine, endLabelName);
            Engine.Add(endLabelCommand);
         }
         else
         {
            Engine.Add(labelCommand);
         }
      }
   }
}
