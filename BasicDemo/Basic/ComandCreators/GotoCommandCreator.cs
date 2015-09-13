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
         var result = (node is TokenTreeNode) && ((node as TokenTreeNode).Token.Type == TokenType.goto_keyword);
         return result;
      }

      public override BasicCommand Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode);
         var children = node.Children.OfType<TokenTreeNode>().ToList();
         var name = children.First().Token.Value;
         var command = new GotoCommand(tokNode.Token, Engine, name);

         return command;
      }
   }
}
