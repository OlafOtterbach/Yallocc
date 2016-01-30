using Yallocc.SyntaxTree;

namespace Yallocc.SyntaxTreeTest.SimpleGrammar
{
   public class SimpleTokenDefinition : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         yacc.DefineTokens()
             .AddTokenPattern(@"A", TokenType.A)
             .AddTokenPattern(@"B", TokenType.B)
             .AddTokenPattern(@"C", TokenType.C)
             .End();
      }
   }
}