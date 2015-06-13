using System.Linq;
using System.Collections.Generic;

namespace YallocSyntaxTree
{
   public class SyntaxTreeBuilder
   {
      private Stack<RecursionLevel> _levels;

      public SyntaxTreeBuilder()
      {
         _levels = new Stack<RecursionLevel>();
      }

      public void Enter()
      {
         var level = new RecursionLevel();
         _levels.Push(level);
      }

      public void CreateParent(SyntaxTreeNode parent)
      {
         if (_levels.Count > 0)
         {
            var level = _levels.Peek();
            if (level.HasParentNode)
            {
               var newLevel = new RecursionLevel() { ParentNode = parent };
               level = _levels.Pop();
               var node = level.CreateNode();
               newLevel.AddChild(node);
               _levels.Push(newLevel);
            }
            else
            {
               level.ParentNode = parent;
            }
         }
      }

      public void AddChild(SyntaxTreeNode child)
      {
         if (_levels.Count > 0)
         {
            _levels.Peek().AddChild(child);
         }
      }

      public SyntaxTreeNode GetLastChild()
      {
         var lastChild = (_levels.Count > 0) ?  _levels.Peek().ChildrenNodes.LastOrDefault() : null;
         return lastChild;
      }

      public void Exit()
      {
         var level = _levels.Pop();
         var node = level.CreateNode();
         if (_levels.Count > 0)
         {
            if (node != null)
            {
               _levels.Peek().AddChild(node);
            }
         }
         else
         {
            Root = node;
         }
      }

      public SyntaxTreeNode Root { get; private set; }
   }
}
