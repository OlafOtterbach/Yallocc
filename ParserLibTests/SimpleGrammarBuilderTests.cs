using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserLib
{
   class Builder
   {
      public Produce Begin()
      {
         return new Produce();
      }

      public BranchProducer Branch()
      {
         return new BranchProducer();
      }
   }

   interface IBranchProducer
   {

   }

   class BranchResult : IBranchProducer
   {
   }

   class BranchProducer : IBranchProducer
   {
      public BranchProducer Add()
      {
         return this;
      }

      public BranchResult Goto()
      {
         return new BranchResult();
      }
   }

   class Produce
   {
      public Produce Add()
      {
         return this;
      }

      public void Goto()
      {
      }

      public Produce Branch(params IBranchProducer[] branches)
      {
         return this;
      }

      public void End() { }
   }


   [TestClass]
   public class GrammarBuilderTests
   {
      [TestMethod]
      public void Test()
      {
         var b = new Builder();
         b.Begin()
          .Add()
          .Branch
           (
             b.Branch().Add().Add(), 
             b.Branch().Add().Goto(),
             b.Branch().Add().Add().Add().Goto()
           )
          .Add()
          .End();
      }
   }
}
