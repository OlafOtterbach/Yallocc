using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BasicDemo.Basic;
using Yallocc.Tokenizer;

namespace BasicDemoTest
{
   [TestClass]
   public class LetCommandTest
   {
      [TestMethod]
      public void ExecuteTest_RealAssignToReal_correctResult()
      {
         var expressionCmd = CreateRealExpression();
         var x = new BasicReal();
         var engine = new BasicEngine();
         engine.RegisterVariable("x", x);
         var letCommand = new LetCommand(new Token<TokenType>("x", 0, 1), engine, "x", expressionCmd);
         engine.Add(letCommand);
         engine.Add(new EndOfProgramCommand(new Token<TokenType>("end", 0, 3), engine));
         engine.Reset();

         letCommand.Execute();

         Assert.AreEqual(2.5, x.Value);
      }

      [TestMethod]
      public void ExecuteTest_RealAssignToInteger_correctResult()
      {
         var expressionCmd = CreateRealExpression();
         var x = new BasicInteger();
         var engine = new BasicEngine();
         engine.RegisterVariable("x", x);
         var letCommand = new LetCommand(new Token<TokenType>("x", 0, 1), engine, "x", expressionCmd);
         engine.Add(letCommand);
         engine.Add(new EndOfProgramCommand(new Token<TokenType>("end", 0, 3), engine));
         engine.Reset();

         letCommand.Execute();

         Assert.AreEqual(2, x.Value);
      }

      [TestMethod]
      public void ExecuteTest_RealAssignToString_correctResult()
      {
         var expressionCmd = CreateRealExpression();
         var x = new BasicString();
         var engine = new BasicEngine();
         engine.RegisterVariable("x", x);
         var letCommand = new LetCommand(new Token<TokenType>("x", 0, 1), engine, "x", expressionCmd);
         engine.Add(letCommand);
         engine.Add(new EndOfProgramCommand(new Token<TokenType>("end", 0, 3), engine));
         engine.Reset();

         letCommand.Execute();

         Assert.AreEqual("2.5", x.Value);
      }

      private ExpressionCommand CreateRealExpression()
      {
         var postorder = new List<BasicEntity> 
         { 
            new BasicReal(1.25),
            new BasicReal(2.0),
            new BasicMultiplication(),
         };
         var expressionCmd = new ExpressionCommand(new Token<TokenType>("0.0", 0, 3), postorder);
         return expressionCmd;
      }
   }
}