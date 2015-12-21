using Yallocc.SyntaxTree;
using Yallocc;

namespace Yallocc.SyntaxTreeTest.ExpressionTree
{
   public class ExpressionTokenDefinition : ITokenAndGrammarDefinition<ExpressionTokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, ExpressionTokenType> yacc)
      {
         yacc.DefineTokens()
             .AddTokenPattern(@"\+", ExpressionTokenType.plus)
             .AddTokenPattern(@"\-", ExpressionTokenType.minus)
             .AddTokenPattern(@"\*", ExpressionTokenType.mult)
             .AddTokenPattern(@"\/", ExpressionTokenType.div)
             .AddTokenPattern(@"=", ExpressionTokenType.equal)
             .AddTokenPattern(@"\>", ExpressionTokenType.greater)
             .AddTokenPattern(@"\<", ExpressionTokenType.less)
             .AddTokenPattern(@"\(", ExpressionTokenType.open)
             .AddTokenPattern(@"\)", ExpressionTokenType.close)
             .AddTokenPattern(@"(0|1|2|3|4|5|6|7|8|9)+(\.(0|1|2|3|4|5|6|7|8|9)+)?", ExpressionTokenType.number)
             .End();
      }
   }
}