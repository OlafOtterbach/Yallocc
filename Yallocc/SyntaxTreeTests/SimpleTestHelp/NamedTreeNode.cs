using SyntaxTree;

namespace SyntaxTreeTest
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