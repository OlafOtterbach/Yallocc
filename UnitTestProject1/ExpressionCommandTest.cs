using System.Linq;
using LexSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YalloccDemo;
using SyntaxTree;
using Yallocc;
using YalloccDemo.Grammar;
using System.Collections.Generic;
using YalloccDemo.Basic;

namespace YalloccDemoTest
{
   [TestClass]
   public class ExpressionCommandTest
   {
      [TestMethod]
      public void Test()
      {
         var postorder = new List<BasicEntity> 
         { 
            new BasicFloat(2.0),
            new BasicFloat(3.0),
            new BasicMultiplication(),
            new BasicFloat(1.0), 
            new BasicAddition()
         };
         var expressionCmd = new ExpressionCommand(postorder);

         var res = expressionCmd.Execute();

         Assert.IsTrue(res.IsFloat);
         var floatRes = res as BasicFloat;
         Assert.AreEqual(floatRes.Value, 7.0);
      }
   }
}
