using Yallocc.Tokenizer;
using Yallocc.SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class LabelStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         // LABEL
         //
         // --(NAME)->-(:)->
         //
         yacc.Grammar("LabelStatement")
             .Enter                          .Action((SyntaxTreeBuilder stb) => stb.Enter()).Name("LabelBegin")
             .Token(TokenType.label)         .Name("Label").Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Exit                           .Action((SyntaxTreeBuilder stb) => stb.Exit()).Name("LabelEnd")
             .EndGrammar();
      }
   }
}