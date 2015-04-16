using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LexSharp;

namespace ParserLib
{
   [TestClass]
   public class ExpressionParserTest
   {
      [TestMethod]
      public void ParseTokensTest_2_Correct()
      {
         var result = CheckText("2");
         Assert.IsTrue(result);
      }

      [TestMethod]
      public void ParseTokensTest_2plus3_Correct()
      {
         var result = CheckText("2+3");
         Assert.IsTrue(result);
      }

      [TestMethod]
      public void ParseTokensTest_2plus3mult4_Correct()
      {
         var result = CheckText("2+3*4");
         Assert.IsTrue(result);
      }

      [TestMethod]
      public void ParseTokensTest_Clamp2plus3Clampmult4_Correct()
      {
         var result = CheckText("(2+3)*4");
         Assert.IsTrue(result);
      }

      [TestMethod]
      public void ParseTokensTest_Clamp2PlusClamp2Plus3ClampMult3ClampMult4_Correct()
      {
         var result = CheckText("(2+(2+3)*3)*4");
         Assert.IsTrue(result);
      }

      [TestMethod]
      public void ParseTokensTest_Clamp2PlusClamp2Plus3Mult3ClampMult4_Correct()
      {
         var result = CheckText("(2+(2+3*3)*4)");
         Assert.IsTrue(result);
      }

      [TestMethod]
      public void ParseTokensTest_ClampClampClampClampClamp2Minus3ClampDiv4ClampClampClampClamp_Correct()
      {
         var result = CheckText("(((((2-3)/4))))");
         Assert.IsTrue(result);
      }

      [TestMethod]
      public void ParseTokensTest_ClampClampClampClampClamp2Minus3ClampDiv4ClampClampClamp_Incorrect()
      {
         var result = CheckText("(((((2-3)/4)))");
         Assert.IsFalse(result);
      }

      [TestMethod]
      public void ParseTokensTest_2Plus_Incorrect()
      {
         var result = CheckText("2+");
         Assert.IsFalse(result);
      }

      private bool CheckText(string text)
      {
         var lex = CreateLex();
         var grammar = CreateExpression();
         var sequence = lex.Scan(text);
         var parser = new Parser<ExpressionTokenType>();
         var result = parser.ParseTokens(grammar, sequence);
         return result;
      }

      private Transition CreateExpression()
      {
         var startExpression = new LabelTransition("StartExpression");
         var expression = new GrammarTransition(startExpression);
         var plus = new TokenTypeTransition<ExpressionTokenType>(ExpressionTokenType.plus);
         var minus = new TokenTypeTransition<ExpressionTokenType>(ExpressionTokenType.minus);
         var term = new GrammarTransition(CreateTerm(startExpression));
         var endExpression = new LabelTransition("EndExpression");
         startExpression.AddSuccessor(plus);
         startExpression.AddSuccessor(minus);
         startExpression.AddSuccessor(term);
         plus.AddSuccessor(term);
         minus.AddSuccessor(term);
         term.AddSuccessor(plus);
         term.AddSuccessor(minus);
         term.AddSuccessor(endExpression);
         return startExpression;
      }

      private Transition CreateTerm(Transition expression)
      {
         var startTerm = new LabelTransition("StartTerm");
         var factor = new GrammarTransition(CreateFactor(expression));
         var mult = new TokenTypeTransition<ExpressionTokenType>(ExpressionTokenType.mult);
         var div = new TokenTypeTransition<ExpressionTokenType>(ExpressionTokenType.div);
         var endTerm = new LabelTransition("EndTerm");
         startTerm.AddSuccessor(factor);
         factor.AddSuccessor(mult);
         factor.AddSuccessor(div);
         factor.AddSuccessor(endTerm);
         mult.AddSuccessor(factor);
         div.AddSuccessor(factor);
         return startTerm;
      }

      private Transition CreateFactor(Transition expression)
      {
         var startFactor = new LabelTransition("StartFactor");
         var number = new TokenTypeTransition<ExpressionTokenType>(ExpressionTokenType.number);
         var clampOpen = new TokenTypeTransition<ExpressionTokenType>(ExpressionTokenType.open_clamp);
         var clampClose = new TokenTypeTransition<ExpressionTokenType>(ExpressionTokenType.close_clamp);
         var expressionContainer = new GrammarTransition(expression);
         var endFactor = new LabelTransition("EndFactor");
         startFactor.AddSuccessor(number);
         startFactor.AddSuccessor(clampOpen);
         number.AddSuccessor(endFactor);
         clampOpen.AddSuccessor(expressionContainer);
         expressionContainer.AddSuccessor(clampClose);
         clampClose.AddSuccessor(endFactor);
         return startFactor;
      }

      private Lex<ExpressionTokenType> CreateLex()
      {
         var lex = new Lex<ExpressionTokenType>();
         lex.Register(@"\+", ExpressionTokenType.plus);
         lex.Register(@"-", ExpressionTokenType.minus);
         lex.Register(@"\*", ExpressionTokenType.mult);
         lex.Register(@"/", ExpressionTokenType.div);
         lex.Register(@"\(", ExpressionTokenType.open_clamp);
         lex.Register(@"\)", ExpressionTokenType.close_clamp);
         lex.Register(@"(0|1|2|3|4|5|6|7|8|9)+", ExpressionTokenType.number);
         return lex;
      }

   }
}
