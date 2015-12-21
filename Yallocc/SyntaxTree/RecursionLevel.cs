using System.Collections.Generic;
using System.Linq;

namespace Yallocc.SyntaxTree
{
   public class RecursionLevel
   {
      private SyntaxTreeNode _parentNode;

      private List<SyntaxTreeNode> _childrenNodes;

      private List<SyntaxTreeNode> _innerNodes;

      public RecursionLevel()
      {
         _parentNode = null;
         _childrenNodes = new List<SyntaxTreeNode>();
         _innerNodes = new List<SyntaxTreeNode>();
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

      public void AddInnerNode(SyntaxTreeNode node)
      {
         if ((node != null) && (!_innerNodes.Contains(node)))
         {
            _innerNodes.Add(node);
            if (node == _parentNode)
            {
               _parentNode = null;
            }
         }
      }

      public void MakeInnerNodesToChildren()
      {
         _childrenNodes.AddRange(_innerNodes);
         _innerNodes.Clear();
      }

      public void MakeInnerNodesToParentAndChildren()
      {
         if(!_innerNodes.Any())
         {
            return;
         }
         ParentNode = _innerNodes.First();
         _childrenNodes.AddRange(_innerNodes.Skip(1));
         _innerNodes.Clear();
      }

      public IEnumerable<SyntaxTreeNode> CreateNodes()
      {
         if (ParentNode != null)
         {
            ParentNode.Children = _childrenNodes;
         }
         var nodes = (ParentNode != null) ? new List<SyntaxTreeNode>{ ParentNode } : _childrenNodes;
         return nodes;
      }
   }
}
