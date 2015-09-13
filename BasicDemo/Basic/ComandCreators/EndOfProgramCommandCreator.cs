using System.Linq;
using SyntaxTree;

namespace BasicDemo.Basic
{
   public class EndOfProgramCommandCreator : CommandCreator
   {
      public EndOfProgramCommandCreator(BasicEngine engine) : base(engine)
      {}

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode) && ((node as TokenTreeNode).Token.Type == TokenType.end_keyword);
         return result;
      }

      public override BasicCommand Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode);
         var command = new EndOfProgramCommand(tokNode.Token, Engine);
         return command;
      }
   }
}
