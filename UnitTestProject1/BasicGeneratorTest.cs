﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YalloccDemo;

namespace YalloccDemoTest
{
   [TestClass]
   public class BasicGeneratorTest
   {
      [TestMethod]
      public void Test()
      {
         var generator = new BasicGenerator();

         var parser = generator.CreateParser();
         Assert.AreNotEqual(parser, null);
         var res = parser.Parse("2+3*4");

         Assert.IsTrue(res.Success);
      }
   }
}
