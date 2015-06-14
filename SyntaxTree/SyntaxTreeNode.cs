using System.Collections.Generic;

namespace SyntaxTree
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
