using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class LabelStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.Grammar("LabelStatement")
             .Enter.Action(() => stb.Enter()).Name("LabelBegin")
             .Token(TokenType.name).Name("LabelName").Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Token(TokenType.colon).Name("LabelColon")
             .Lambda.Action(() => stb.CreateParent(new TokenTreeNode(new Token<TokenType>(TokenType.label))))
             .Exit.Action(() => stb.Exit()).Name("LabelEnd")
             .EndGrammar();
      }
   }
}