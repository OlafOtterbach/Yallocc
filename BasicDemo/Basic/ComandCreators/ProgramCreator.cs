using SyntaxTree;
using System.Linq;

namespace BasicDemo.Basic
{
   public class ProgramCreator : CommandCreator
   {
      public ProgramCreator(BasicEngine engine) : base(engine)
      {
      }

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode) && ((node as TokenTreeNode).Token.Type == TokenType.program_keyword);
         return result;
      }

      public override void Create(SyntaxTreeNode root)
      {
         var nodes = root.Children.Skip(1);
         var creator = new StatementSequenceCreator(Engine);
         creator.Create(nodes);
      }
   }
}
