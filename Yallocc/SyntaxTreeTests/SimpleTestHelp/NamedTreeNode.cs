using Yallocc.SyntaxTree;

namespace Yallocc.SyntaxTreeTest
{
   public class NamedTreeNode : SyntaxTreeNode
   {
      public NamedTreeNode(string name)
      {
         Name = name;
      }

      public string Name  { get; private set; }
   }
}