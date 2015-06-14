﻿using System.Linq;
using System.Collections.Generic;

namespace SyntaxTree
{
   public class RecursionLevel
   {
      private List<SyntaxTreeNode> _childrenNodes;

      public RecursionLevel()
      {
         ParentNode = null;
         _childrenNodes = new List<SyntaxTreeNode>();
      }

      public IEnumerable<SyntaxTreeNode> ChildrenNodes
      {
         get
         { 
            return _childrenNodes; 
         }
      }

      public SyntaxTreeNode ParentNode { get; set; }

      public bool HasParentNode
      {
         get
         {
            return ParentNode != null;
         }
      }

      public void AddChild(SyntaxTreeNode child)
      {
         _childrenNodes.Add(child);
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
