using Yallocc.SyntaxTree;
using Yallocc;
using Yallocc.Tokenizer;

namespace BasicDemo.Basic
{
   public class ExpressionGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         // Expression
         //                                               |---------[Relation]------>----|
         //                                               |                              |
         //                                               |-----[SimpleExpression]-->----|
         //                                               |                             \|/
         // --"ExpressionStart"-->--[SimpleExpression]----------------------------------------->
         //
         yacc.Grammar("Expression")
             .Enter.Name("ExpressionStart")       .Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Gosub("SimpleExpression")           .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Gosub("Relation")           .Action((SyntaxTreeBuilder stb) => stb.CapInnerNodeToParent())
                     .Gosub("SimpleExpression")   .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch.Default
              )
             .Exit                                .Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();

         // Relation
         //                         |-------equal-->--|
         //                         |                 |
         //                         |------greater->--|
         //                         |                \|/
         // --"RelationStart"--->-----------less--->--------->
         //
         yacc.Grammar("Relation")
             .Enter.Name("RelationStart")              .Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Switch
              (
                 yacc.Branch.Token(TokenType.equal)    .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch.Token(TokenType.greater)  .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch.Token(TokenType.less)     .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
              )
             .Exit                                     .Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();

         // SimpleExpression
         //
         //                            |--plus---|            |-------------------plus----|
         //                            |         |            |                           |
         //                            |--minus--|            |-------------------minus---|
         //                            |        \|/          \|/                          |
         // --"SimpleExpressionStart"----------------"SimpleExpreesionLoop"------[Term]---------------->
         //
         yacc.Grammar("SimpleExpression")
             .Enter                                        .Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Label("SimpleExpressionStart")
             .Switch
              (
                yacc.Branch.Token(TokenType.plus)          .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                yacc.Branch.Token(TokenType.minus)         .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                yacc.Branch.Token(TokenType.not_keyword)   .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                yacc.Branch.Default
              )
             .Label("SimpleExpressionLoop")
             .Gosub("Term")                                .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.plus)                 .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("SimpleExpressionLoop"),
                yacc.Branch
                    .Token(TokenType.or_keyword)           .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("SimpleExpressionLoop"),
                yacc.Branch
                    .Token(TokenType.minus)                .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("SimpleExpressionLoop"),
                yacc.Branch.Default
              )
             .Exit                                        .Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();

         // Term
         //           |------------mult---------|
         //           |                         |
         //           |------------div----------|
         //          \|/                        |               
         // -----"TermStart"-->--[Factor]-------------->
         //
         yacc.Grammar("Term")
             .Enter                                   .Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Label("TermStart")
             .Gosub("Factor")                         .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.mult)            .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(TokenType.div)             .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(TokenType.and_keyword)     .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(TokenType.mod_keyword)     .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                    .Goto("TermStart"),
                yacc.Branch.Default
              )
             .Exit                                    .Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();

         // Factor
         //                                             
         //                      |---------number------->---|
         //                      |                          |
         // --"FactorStart"-->-------(-[Expression]-)--->- \|/----->
         //
         yacc.Grammar("Factor")
             .Enter.Name("FactorStart")                      .Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.integer)               .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch
                     .Token(TokenType.real)                  .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok))),
                 yacc.Branch
                     .Token(TokenType.open)
                     .Gosub("Expression")                    .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
                     .Token(TokenType.close),
                 yacc.Branch
                     .Token(TokenType.name)                  .Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
                     .Switch
                      (
                         yacc.Branch
                             .Token(TokenType.open)
                             .Label("ParamList")
                             .Gosub("Expression")            .Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
                             .Switch
                              (
                                 yacc.Branch
                                     .Token(TokenType.comma)
                                     .Goto("ParamList"),
                                 yacc.Branch.Default
                              )
                             .Token(TokenType.close),
                         yacc.Branch.Default
                      )
               )
             .Exit                                           .Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}
