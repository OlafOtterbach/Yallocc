using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxTree;
using SyntaxTreeTest.ExpressionTree;
using System.Linq;

namespace SyntaxTreeTest
{
   [TestClass]
   public class RecursionLevelTests
   {
      [TestMethod]
      public void ConstructorTest()
      {
         var level = new RecursionLevel();

         Assert.IsFalse(level.HasParentNode);
         Assert.IsNull(level.ParentNode);
         Assert.IsFalse(level.ChildrenNodes.Any());
      }

      [TestMethod]
      public void AddChildTest_Null_EmptyChildNodeList()
      {
         var level = new RecursionLevel();

         level.AddChild(null);

         Assert.IsFalse(level.ChildrenNodes.Any());
      }

      [TestMethod]
      public void AddChildTest_OneChildAdded_ChildNodeListWithOneChild()
      {
         var level = new RecursionLevel();

         const string NAME = "One";
         level.AddChild(new NamedTreeNode(NAME));

         Assert.AreEqual(level.ChildrenNodes.Count(),1);
         Assert.AreEqual((level.ChildrenNodes.First() as NamedTreeNode).Name, NAME);
      }

      [TestMethod]
      public void ParentNodeTest_OnePartentAdded_NoChildrenOneParent()
      {
         var level = new RecursionLevel();

         const string NAME = "One";
         level.ParentNode = new NamedTreeNode(NAME);

         Assert.AreEqual(level.ChildrenNodes.Count(), 0);
         Assert.AreEqual((level.ParentNode as NamedTreeNode).Name, NAME);
      }

      [TestMethod]
      public void AddChildTest_ThreeChildsAdded_TwoChildInList()
      {
         var level = new RecursionLevel();

         const string NAME1 = "One";
         const string NAME2 = "Two";
         var one = new NamedTreeNode(NAME1);
         var two = new NamedTreeNode(NAME2);
         level.AddChild(one);
         level.AddChild(two);
         level.AddChild(one);

         Assert.AreEqual(level.ChildrenNodes.Count(), 2);
         Assert.AreEqual((level.ChildrenNodes.First() as NamedTreeNode).Name, NAME1);
         Assert.AreEqual((level.ChildrenNodes.Last() as NamedTreeNode).Name, NAME2);
      }

      [TestMethod]
      public void ParentNodeAndAddChildTest_MakeChildToParent_ParentIsNoChild()
      {
         var level = new RecursionLevel();

         const string NAME1 = "One";
         const string NAME2 = "Two";
         var one = new NamedTreeNode(NAME1);
         var two = new NamedTreeNode(NAME2);
         level.AddChild(one);
         level.AddChild(two);
         level.ParentNode = one;

         Assert.AreEqual(level.ChildrenNodes.Count(), 1);
         Assert.AreEqual((level.ChildrenNodes.First() as NamedTreeNode).Name, NAME2);
         Assert.AreEqual((level.ParentNode as NamedTreeNode).Name, NAME1);
      }

      [TestMethod]
      public void ParentNodeAndAddChildTest_MakeParentToChild_ChildIsNotParent()
      {
         const string NAME1 = "One";
         const string NAME2 = "Two";

         var one = new NamedTreeNode(NAME1);
         var two = new NamedTreeNode(NAME2);
         var level = new RecursionLevel();
         level.ParentNode = one;
         level.AddChild(one);
         level.AddChild(two);

         Assert.AreEqual(level.ChildrenNodes.Count(), 2);
         Assert.AreEqual((level.ChildrenNodes.First() as NamedTreeNode).Name, NAME1);
         Assert.AreEqual((level.ChildrenNodes.Last() as NamedTreeNode).Name, NAME2);
         Assert.IsFalse(level.HasParentNode);
         Assert.IsNull(level.ParentNode);
      }

      [TestMethod]
      public void CreateNodeTest_OneParentTwoChildren_NodeWithTwoChildren()
      {
         const string NAME1 = "One";
         const string NAME2 = "Two";
         const string NAME3 = "Three";

         var one = new NamedTreeNode(NAME1);
         var two = new NamedTreeNode(NAME2);
         var three = new NamedTreeNode(NAME3);
         var level = new RecursionLevel();
         level.ParentNode = one;
         level.AddChild(two);
         level.AddChild(three);

         var node = level.CreateNode() as NamedTreeNode;

         Assert.IsNotNull(node);
         Assert.AreEqual(node.Name, NAME1);
         Assert.AreEqual((node.Children.First() as NamedTreeNode).Name, NAME2);
         Assert.AreEqual((node.Children.Last() as NamedTreeNode).Name, NAME3);
      }

      [TestMethod]
      public void CreateNodeTest_NoParentTwoChildren_FirstAsNodeWithTwoChildren()
      {
         const string NAME1 = "One";
         const string NAME2 = "Two";
         const string NAME3 = "Three";

         var one = new NamedTreeNode(NAME1);
         var two = new NamedTreeNode(NAME2);
         var three = new NamedTreeNode(NAME3);
         var level = new RecursionLevel();
         level.AddChild(one);
         level.AddChild(two);
         level.AddChild(three);

         var node = level.CreateNode() as NamedTreeNode;

         Assert.IsNotNull(node);
         Assert.AreEqual(node.Name, NAME1);
         Assert.AreEqual((node.Children.First() as NamedTreeNode).Name, NAME2);
         Assert.AreEqual((node.Children.Last() as NamedTreeNode).Name, NAME3);
      }
   }
}