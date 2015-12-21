using System.Linq;
using Yallocc.SyntaxTree;

namespace BasicDemo.Basic
{
   public class DimCommandCreator : CommandCreator
   {
      public DimCommandCreator(BasicEngine engine) : base(engine)
      {}

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode<TokenType>) && ((node as TokenTreeNode<TokenType>).Token.Type == TokenType.dim_keyword);
         return result;
      }

      public override void Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode<TokenType>);
         var children = node.Children.OfType<TokenTreeNode<TokenType>>().ToList();
         var name = children.First().Token.Value;
         var expressions = children.Skip(1)
                                    .TakeWhile(x => x.Token.Type != TokenType.close)
                                    .Select(n => new ExpressionCommandCreator(Engine).Create(n))
                                    .ToArray();
         var command = new DimCommand(tokNode.Token, Engine, name, expressions);
         Engine.Add(command);
      }
   }
}
