using System.Linq;
using System.Collections.Generic;

namespace SyntaxTree
{
   public class RecursionLevel
   {
      private SyntaxTreeNode _parentNode;

      private List<SyntaxTreeNode> _childrenNodes;

      public RecursionLevel()
      {
         _parentNode = null;
         _childrenNodes = new List<SyntaxTreeNode>();
      }

      public IEnumerable<SyntaxTreeNode> ChildrenNodes
      {
         get
         { 
            return _childrenNodes; 
         }
      }

      public SyntaxTreeNode ParentNode 
      { 
         get
         {
            return _parentNode;
         }
         set
         {
            _parentNode = value;
            if( _childrenNodes.Contains(_parentNode))
            {
               _childrenNodes.Remove(_parentNode);
            }
         }
      }

      public bool HasParentNode
      {
         get
         {
            return ParentNode != null;
         }
      }

      public void AddChild(SyntaxTreeNode child)
      {
         if ((child != null) && (!_childrenNodes.Contains(child)))
         {
            _childrenNodes.Add(child);
            if(child == _parentNode)
            {
               _parentNode = null;
            }
         }
      }

      public SyntaxTreeNode CreateNode()
      {
         if (ParentNode != null)
         {
            ParentNode.Children = _childrenNodes;
         }
         else  if(_childrenNodes.Any())
         {
            ParentNode = _childrenNodes.First();
            ParentNode.Children = ParentNode.Children.Concat(_childrenNodes.Where(x => x != ParentNode));
         }
         return ParentNode;
      }
   }
}
