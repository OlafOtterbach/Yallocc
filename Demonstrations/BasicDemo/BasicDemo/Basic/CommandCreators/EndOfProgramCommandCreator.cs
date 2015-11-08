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
         var result = (node is TokenTreeNode<TokenType>) && ((node as TokenTreeNode<TokenType>).Token.Type == TokenType.end_keyword);
         return result;
      }

      public override void Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode<TokenType>);
         var command = new EndOfProgramCommand(tokNode.Token, Engine);
         Engine.Add(command);
      }
   }
}
