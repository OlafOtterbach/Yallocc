using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yallocc.Tokenizer;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc
{
   [TestClass]
   public class ExpressionParserTest
   {
      private class DummyContext {}

      private enum ExpressionTokenType
      {
         open_clamp,
         close_clamp,
         plus,
         minus,
         mult,
         div,
         number
      }

      [TestMethod]
      public void ParseTokensTest_2_Correct()
      {
         var result = CheckText("2");
         Assert.IsTrue(result.Success);
      }

      [TestMethod]
      public void ParseTokensTest_2plus3_Correct()
      {
         var result = CheckText("2+3");
         Assert.IsTrue(result.Success);
      }

      [TestMethod]
      public void ParseTokensTest_2plus3mult4_Correct()
      {
         var result = CheckText("2+3*4");
         Assert.IsTrue(result.Success);
      }

      [TestMethod]
      public void ParseTokensTest_Clamp2plus3Clampmult4_Correct()
      {
         var result = CheckText("(2+3)*4");
         Assert.IsTrue(result.Success);
      }

      [TestMethod]
      public void ParseTokensTest_Clamp2PlusClamp2Plus3ClampMult3ClampMult4_Correct()
      {
         var result = CheckText("(2+(2+3)*3)*4");
         Assert.IsTrue(result.Success);
      }

      [TestMethod]
      public void ParseTokensTest_Clamp2PlusClamp2Plus3Mult3ClampMult4_Correct()
      {
         var result = CheckText("(2+(2+3*3)*4)");
         Assert.IsTrue(result.Success);
      }

      [TestMethod]
      public void ParseTokensTest_ClampClampClampClampClamp2Minus3ClampDiv4ClampClampClampClamp_Correct()
      {
         var result = CheckText("(((((2-3)/4))))");
         Assert.IsTrue(result.Success);
      }

      [TestMethod]
      public void ParseTokensTest_ClampClampClampClampClamp2Minus3ClampDiv4ClampClampClamp_Incorrect()
      {
         var result = CheckText("(((((2-3)/4)))");
         Assert.IsFalse(result.Success);
         Assert.IsFalse(result.SyntaxError);
         Assert.IsTrue(result.GrammarOfTextNotComplete);
         Assert.AreEqual(13,result.Position);
      }

      [TestMethod]
      public void ParseTokensTest_2Plus_Incorrect()
      {
         var result = CheckText("2+");
         Assert.IsFalse(result.SyntaxError);
         Assert.IsTrue(result.GrammarOfTextNotComplete);
         Assert.AreEqual(1,result.Position);
      }

      private ParserResult CheckText(string text)
      {
         var tokenizer = Createtokenizer();
         var grammar = CreateExpression();
         var sequence = tokenizer.Scan(text);
         var parser = new SyntaxDiagramParser<DummyContext,ExpressionTokenType>(grammar);
         var result = parser.ParseTokens(sequence, new DummyContext());
         return result;
      }

      private Transition CreateExpression()
      {
         var startExpression = new LabelTransition<DummyContext>("StartExpression");
         var expression = new GrammarTransition<DummyContext>(startExpression);
         var plus = new TokenTypeTransition<DummyContext,ExpressionTokenType>(ExpressionTokenType.plus);
         var minus = new TokenTypeTransition<DummyContext,ExpressionTokenType>(ExpressionTokenType.minus);
         var term = new GrammarTransition<DummyContext>(CreateTerm(startExpression));
         var endExpression = new LabelTransition<DummyContext>("EndExpression");
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
         var startTerm = new LabelTransition<DummyContext>("StartTerm");
         var factor = new GrammarTransition<DummyContext>(CreateFactor(expression));
         var mult = new TokenTypeTransition<DummyContext,ExpressionTokenType>(ExpressionTokenType.mult);
         var div = new TokenTypeTransition<DummyContext,ExpressionTokenType>(ExpressionTokenType.div);
         var endTerm = new LabelTransition<DummyContext>("EndTerm");
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
         var startFactor = new LabelTransition<DummyContext>("StartFactor");
         var number = new TokenTypeTransition<DummyContext,ExpressionTokenType>(ExpressionTokenType.number);
         var clampOpen = new TokenTypeTransition<DummyContext,ExpressionTokenType>(ExpressionTokenType.open_clamp);
         var clampClose = new TokenTypeTransition<DummyContext,ExpressionTokenType>(ExpressionTokenType.close_clamp);
         var expressionContainer = new GrammarTransition<DummyContext>(expression);
         var endFactor = new LabelTransition<DummyContext>("EndFactor");
         startFactor.AddSuccessor(number);
         startFactor.AddSuccessor(clampOpen);
         number.AddSuccessor(endFactor);
         clampOpen.AddSuccessor(expressionContainer);
         expressionContainer.AddSuccessor(clampClose);
         clampClose.AddSuccessor(endFactor);
         return startFactor;
      }

      private Tokenizer<ExpressionTokenType> Createtokenizer()
      {
         var tokenizerCreator = new LeTokCreator<ExpressionTokenType>();
         tokenizerCreator.Register(@"\+", ExpressionTokenType.plus);
         tokenizerCreator.Register(@"-", ExpressionTokenType.minus);
         tokenizerCreator.Register(@"\*", ExpressionTokenType.mult);
         tokenizerCreator.Register(@"/", ExpressionTokenType.div);
         tokenizerCreator.Register(@"\(", ExpressionTokenType.open_clamp);
         tokenizerCreator.Register(@"\)", ExpressionTokenType.close_clamp);
         tokenizerCreator.Register(@"(0|1|2|3|4|5|6|7|8|9)+", ExpressionTokenType.number);
         return tokenizerCreator.Create();
      }
   }
}
