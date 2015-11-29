using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class LabelStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         // LABEL
         //
         // --(NAME)->-(:)->
         //
         yacc.Grammar("LabelStatement")
             .Enter                          .Action(() => stb.Enter()).Name("LabelBegin")
             .Token(TokenType.label)         .Name("Label").Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Exit                           .Action(() => stb.Exit()).Name("LabelEnd")
             .EndGrammar();
      }
   }
}