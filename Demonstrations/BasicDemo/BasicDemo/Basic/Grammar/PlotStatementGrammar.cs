using Yallocc.Tokenizer;
using Yallocc.SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class PlotStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         // PLOT
         //
         // --(PLOT)->-(x number)->-(,)->-(y number)->-(,)->-(color number)->-(,)->-
         //
         yacc.Grammar("PlotStatement")
             .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Token(TokenType.plot_keyword)    .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
             .Gosub("Expression")              .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Token(TokenType.comma)
             .Gosub("Expression")              .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Token(TokenType.comma)
             .Gosub("Expression")              .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}
