using Yallocc.Tokenizer;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class DimStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         // DIM
         // 
         //                        |-------(,)------|
         //                       \|/               |
         // --(DIM)-->--(name)-->-----[Expression]------>
         //
         yacc.Grammar("DimStatement")
             .Enter                           .Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Token(TokenType.dim_keyword)    .Action((SyntaxTreeBuilder stb, Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
             .Token(TokenType.name)           .Action((SyntaxTreeBuilder stb, Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Token(TokenType.open)
             .Label("ParamList")
             .Gosub("Expression")             .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.comma)
                     .Goto("ParamList"),
                 yacc.Branch.Default
              )
             .Token(TokenType.close)
             .Exit                            .Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}