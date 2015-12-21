using Yallocc.Tokenizer;
using Yallocc.SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class GotoStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         // GOTO
         //
         // --(GOTO)->-(name)->
         //
         yacc.Grammar("GotoStatement")
             .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Token(TokenType.goto_keyword)    .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
             .Token(TokenType.name)            .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}
