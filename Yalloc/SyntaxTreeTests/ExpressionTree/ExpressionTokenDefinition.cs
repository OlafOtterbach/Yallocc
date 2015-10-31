using SyntaxTree;
using Yallocc;

namespace SyntaxTreeTest.ExpressionTree
{
   public class ExpressionTokenDefinition : ITokenAndGrammarDefinition<ExpressionTokenType>
   {
      public void Define(Yallocc<ExpressionTokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.AddToken(@"\+", ExpressionTokenType.plus);
         yacc.AddToken(@"\-", ExpressionTokenType.minus);
         yacc.AddToken(@"\*", ExpressionTokenType.mult);
         yacc.AddToken(@"\/", ExpressionTokenType.div);
         yacc.AddToken(@"=", ExpressionTokenType.equal);
         yacc.AddToken(@"\>", ExpressionTokenType.greater);
         yacc.AddToken(@"\<", ExpressionTokenType.less);
         yacc.AddToken(@"\(", ExpressionTokenType.open);
         yacc.AddToken(@"\)", ExpressionTokenType.close);
         yacc.AddToken(@"(0|1|2|3|4|5|6|7|8|9)+(\.(0|1|2|3|4|5|6|7|8|9)+)?", ExpressionTokenType.number);
      }
   }
}