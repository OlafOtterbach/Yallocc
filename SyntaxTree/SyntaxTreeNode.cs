using System.Collections.Generic;

namespace YallocSyntaxTree
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
