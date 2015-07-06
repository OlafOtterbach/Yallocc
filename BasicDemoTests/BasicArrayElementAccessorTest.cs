using System.Linq;
using LexSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicDemo;
using BasicDemo.Grammar;
using BasicDemo.Basic;
using System.Collections.Generic;

namespace BasicDemoTest
{
   [TestClass]
   public class BasicArrayElementAccessorTest
   {
      [TestMethod]
      public void TypeValueTest_AccessElementThree_SetValue()
      {
         var postorder = new List<BasicEntity> 
         {
            new BasicInteger(1),
            new BasicInteger(2),
            new BasicAddition(),
         };
         var expressionCmd = new ExpressionCommand(postorder);
         var array = new BasicArray(5);
         array.Set(new BasicInteger(77), 3);
         var elementAccessor = new BasicArrayElementAccessor(array);
         elementAccessor.Add(expressionCmd);

         Assert.IsTrue(elementAccessor.IsInteger);
         var result = (elementAccessor.Value) as BasicInteger;

         Assert.IsNotNull(result);
         Assert.AreEqual(77, result.Value);
      }

      [TestMethod]
      public void ValueTest_AccessElementThreeTwoFour_SetValue()
      {
         var postorder1 = new List<BasicEntity> { new BasicInteger(3) };
         var postorder2 = new List<BasicEntity> { new BasicInteger(2) };
         var postorder3 = new List<BasicEntity> { new BasicInteger(4) };
         var expressionCmd1 = new ExpressionCommand(postorder1);
         var expressionCmd2 = new ExpressionCommand(postorder2);
         var expressionCmd3 = new ExpressionCommand(postorder3);
         var array = new BasicArray(5, 5, 5);
         var elementAccessor = new BasicArrayElementAccessor(array);
         elementAccessor.Add(expressionCmd1);
         elementAccessor.Add(expressionCmd2);
         elementAccessor.Add(expressionCmd3);
         array.Set(new BasicInteger(77), 3,2,4);

         Assert.IsTrue(elementAccessor.IsInteger);
         var result = (elementAccessor.Value) as BasicInteger;

         Assert.IsNotNull(result);
         Assert.AreEqual(77, result.Value);
      }
   }
}
