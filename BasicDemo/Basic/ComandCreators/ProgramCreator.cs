using SyntaxTree;
using System.Linq;

namespace BasicDemo.Basic
{
   public class ProgramCreator
   {
      private StatementSequenceCreator _creator;

      public ProgramCreator(BasicEngine engine)
      {
         _creator = new StatementSequenceCreator(engine);
      }

      public void Create(SyntaxTreeNode root)
      {
         var nodes = root.Children.Skip(1);
         _creator.Create(nodes);
      }
   }
}
