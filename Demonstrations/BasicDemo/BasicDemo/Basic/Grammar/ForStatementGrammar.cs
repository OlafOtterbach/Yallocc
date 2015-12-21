using Yallocc.SyntaxTree;
using Yallocc;
using Yallocc.Tokenizer;

namespace BasicDemo.Basic
{
   public class ForStatementGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         // FOR TO DO END
         //
         //                                                              -->-(STEP)->--[Expression]---
         //                                                              |                           |
         //                                                              |                          \|/
         // --(FOR)->-(name)->-(=)->-[Expression]->-(TO)->-[Expression]--------------------------------->--(DO)-->
         //
         //
         // -->--[STatementSequence]-->--(END)--->
         //
         yacc.Grammar("ForStatement")
             .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Token(TokenType.for_keyword)                  .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
             .Token(TokenType.name)                         .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Token(TokenType.equal)                        .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Gosub("Expression")                           .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Token(TokenType.to_keyword)                   .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Gosub("Expression")                           .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch.Token(TokenType.step_keyword)
                            .Gosub("Expression")            .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch.Default
              )
             .Token(TokenType.do_keyword)                   .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(new Token<TokenType>("1", TokenType.integer, 0, 1))))
             .Gosub("StatementSequence")                    .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Token(TokenType.end_keyword)                  .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}