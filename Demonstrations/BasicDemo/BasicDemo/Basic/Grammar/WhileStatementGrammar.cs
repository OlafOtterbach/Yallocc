using SyntaxTree;
using Yallocc;
using Yallocc.Tokenizer;

namespace BasicDemo.Basic
{
   public class WhileStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         // WHILE DO END
         //
         // --(WHILE)->-[Expression]->-(DO)->-[StatementSequence]->-(END)-->
         //
         yacc.Grammar("WhileStatement")
             .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Token(TokenType.while_keyword).Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
             .Gosub("Expression").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Token(TokenType.do_keyword).Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Gosub("StatementSequence").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Token(TokenType.end_keyword).Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}