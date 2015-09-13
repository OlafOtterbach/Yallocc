using SyntaxTree;
using System.Linq;

namespace BasicDemo.Basic
{
   public class LabelCommandCreator : CommandCreator
   {
      public LabelCommandCreator(BasicEngine engine) : base(engine)
      { }

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode) && ((node as TokenTreeNode).Token.Type == TokenType.label);
         return result;
      }

      public override BasicCommand Create(SyntaxTreeNode node)
      {
         var tokNode = node as TokenTreeNode;
         var children = node.Children.OfType<TokenTreeNode>().ToList();
         var name = children.First().Token.Value;
         var command = new LabelCommand(tokNode.Token, Engine, name);

         return command;
      }
   }
}
