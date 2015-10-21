using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class PlotStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.Grammar("PlotStatement")
             .Enter.Action(() => stb.Enter())
             .Token(TokenType.plot_keyword)    .Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
             .Gosub("Expression")              .Action(() => stb.AdoptInnerNodes())
             .Token(TokenType.comma)
             .Gosub("Expression")              .Action(() => stb.AdoptInnerNodes())
             .Token(TokenType.comma)
             .Gosub("Expression")              .Action(() => stb.AdoptInnerNodes())
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}
