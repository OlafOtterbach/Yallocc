using System.Linq;
using System.Collections.Generic;

namespace SyntaxTree
{
   public class SyntaxTreeBuilder
   {
      private Stack<RecursionLevel> _levels;

      public SyntaxTreeBuilder()
      {
         _levels = new Stack<RecursionLevel>();
      }

      public void Reset()
      {
         _levels.Clear();
         Root = null;
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
               var nodes = level.CreateNodes().ToList();
               nodes.ForEach(node => newLevel.AddChild(node));
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
         if (_levels.Count > 0)
         {
            var level = _levels.Pop();
            var nodes = level.CreateNodes().ToList();
            if (_levels.Count > 0)
            {
               nodes.ForEach(node => _levels.Peek().AddChild(node));
            }
            else
            {
               // ToDo: Exception werfen
               Root = (nodes.Count == 1) ? nodes.First() : null;
            }
         }
      }

      public SyntaxTreeNode Root { get; private set; }
   }
}
