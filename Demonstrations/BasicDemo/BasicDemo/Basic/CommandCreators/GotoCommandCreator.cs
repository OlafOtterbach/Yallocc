using SyntaxTree;
using System.Linq;

namespace BasicDemo.Basic
{
   public class GotoCommandCreator : CommandCreator
   {
      public GotoCommandCreator(BasicEngine engine) : base(engine)
      { }

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode<TokenType>) && ((node as TokenTreeNode<TokenType>).Token.Type == TokenType.goto_keyword);
         return result;
      }

      public override void Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode<TokenType>);
         var children = node.Children.OfType<TokenTreeNode<TokenType>>().ToList();
         var name = children.First().Token.Value;
         var command = new GotoCommand(tokNode.Token, Engine, name);
         Engine.Add(command);
      }
   }
}
