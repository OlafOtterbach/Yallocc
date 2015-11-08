using System.Linq;
using SyntaxTree;

namespace BasicDemo.Basic
{
   public class LetCommandCreator : CommandCreator
   {
      public LetCommandCreator(BasicEngine engine) : base(engine)
      {}

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode<TokenType>) && ((node as TokenTreeNode<TokenType>).Token.Type == TokenType.let_keyword);
         return result;
      }

      public override void Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode<TokenType>);
         var children = node.Children.OfType<TokenTreeNode<TokenType>>().ToList();
         var name = children.First().Token.Value;
         var expressions = children.Skip(2)
                                    .TakeWhile(x => x.Token.Type != TokenType.close)
                                    .Select(n => new ExpressionCommandCreator(Engine).Create(n))
                                    .ToList();
         var expressionCreator = new ExpressionCommandCreator(Engine);
         var expression = expressionCreator.Create(children.Last());
         var command = new LetCommand(tokNode.Token, Engine, name, expressions, expression);
         Engine.Add(command);
      }
   }
}
