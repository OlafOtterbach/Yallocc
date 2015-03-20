using System.Collections.Generic;

namespace ParserLib
{
    public class SyntaxNode
    {
       public SyntaxNode()
       {
          Successors = new List<SyntaxNode>();
       }

       public virtual IEnumerable<SyntaxNode> Successors { get; private set; }
    }
}
