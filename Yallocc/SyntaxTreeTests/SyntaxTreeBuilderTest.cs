using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.SyntaxTree;
using System.Linq;

namespace Yallocc.SyntaxTreeTest
{
   [TestClass]
   public class SyntaxTreeBuilderTest
   {
      [TestMethod]
      public void ConstructorTest_Nothing_RootIsEmpty ()
      {
         var builder = new SyntaxTreeBuilder();

         Assert.IsNull(builder.Root);
      }

      [TestMethod]
      public void EnterExitTest_EnterAndExit_RootIsNull()
      {
         var builder = new SyntaxTreeBuilder();

         builder.Enter();
         builder.Exit();

         Assert.IsNull(builder.Root);
      }

      [TestMethod]
      public void AddChild_NoEnterOneChild_RootIsNull()
      {
         var builder = new SyntaxTreeBuilder();

         builder.AddChild(new SyntaxTreeNode());
         builder.Exit();

         Assert.IsNull(builder.Root);
      }

      [TestMethod]
      public void AddChild_EnterAndOneChild_RootIsNotNull()
      {
         var builder = new SyntaxTreeBuilder();

         builder.Enter();
         builder.AddChild(new SyntaxTreeNode());
         builder.Exit();

         Assert.IsNotNull(builder.Root);
      }

      [TestMethod]
      public void AddChildTest_TwoChildren_RootIsNull()
      {
         var builder = new SyntaxTreeBuilder();

         builder.Enter();
         builder.AddChild(new NamedTreeNode("One"));
         builder.AddChild(new NamedTreeNode("Two"));
         builder.Exit();

         Assert.IsNull(builder.Root);
      }

      [TestMethod]
      public void CapInnerNodeToParentTest_Nothing_LastChildIsNull()
      {
         var builder = new SyntaxTreeBuilder();
         builder.Enter();

         builder.CapInnerNodeToParent();

         Assert.IsNull(builder.Root);
      }

      [TestMethod]
      public void CreateParentTest_TwoChildrenOneParent_RootIsParent()
      {
         var builder = new SyntaxTreeBuilder();
         builder.Enter();
         var one = new NamedTreeNode("One");
         var two = new NamedTreeNode("Two");
         builder.AddChild(one);
         builder.AddChild(two);
         var three = new NamedTreeNode("Three");
         builder.CreateParent(three);
         builder.Exit();

         Assert.AreEqual(three, builder.Root);
         Assert.AreEqual(one, builder.Root.Children.First());
         Assert.AreEqual(two, builder.Root.Children.Last());
      }

      [TestMethod]
      public void CreateParentTest_OneChildrenTwoParentAdded_TreeWithDepthThreeLastParentIsRoot()
      {
         var builder = new SyntaxTreeBuilder();
         builder.Enter();
         var one = new NamedTreeNode("One");
         var two = new NamedTreeNode("Two");
         builder.AddChild(one);
         builder.CreateParent(two);
         var three = new NamedTreeNode("Three");
         builder.CreateParent(three);
         builder.Exit();

         Assert.AreEqual(three, builder.Root);
         Assert.AreEqual(1, builder.Root.Children.Count());
         Assert.AreEqual(two, builder.Root.Children.First());
         Assert.AreEqual(1, two.Children.Count());
         Assert.AreEqual(one, two.Children.First());
      }
   }
}