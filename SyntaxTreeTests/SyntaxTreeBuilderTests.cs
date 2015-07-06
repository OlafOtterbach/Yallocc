using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxTree;
using SyntaxTreeTest.ExpressionTree;
using System.Linq;

namespace SyntaxTreeTest
{
   [TestClass]
   public class SyntaxTreeBuilderTests
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
      public void AddChildTest_TwoChildren_OneIsRootSecondIsChild()
      {
         var builder = new SyntaxTreeBuilder();

         builder.Enter();
         builder.AddChild(new NamedTreeNode("One"));
         builder.AddChild(new NamedTreeNode("Two"));
         builder.Exit();

         Assert.IsNotNull(builder.Root);
         var one = builder.Root as NamedTreeNode;
         Assert.AreEqual("One", one.Name);
         Assert.AreEqual(1, one.Children.Count());
         var two = one.Children.First() as NamedTreeNode;
         Assert.AreEqual("Two", two.Name);
      }

      [TestMethod]
      public void GetLastChildTest_Nothing_LastChildIsNull()
      {
         var builder = new SyntaxTreeBuilder();
         builder.Enter();

         var last = builder.GetLastChild();

         Assert.IsNull(last);
      }

      [TestMethod]
      public void GetLastChildTest_ThreeChildren_LastChildIsThirdChild()
      {
         var builder = new SyntaxTreeBuilder();
         builder.Enter();
         builder.AddChild(new NamedTreeNode("One"));
         builder.AddChild(new NamedTreeNode("Two"));
         var three = new NamedTreeNode("Three");
         builder.AddChild(three);

         var last = builder.GetLastChild();

         Assert.AreEqual(three, last);
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