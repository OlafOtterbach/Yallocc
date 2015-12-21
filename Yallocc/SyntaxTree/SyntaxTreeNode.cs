using System.Collections.Generic;

namespace Yallocc.SyntaxTree
{
   public class SyntaxTreeNode
   {
      public SyntaxTreeNode()
      {
         Children = new List<SyntaxTreeNode>();
      }

      public IEnumerable<SyntaxTreeNode> Children { get; set; }
   }
}
