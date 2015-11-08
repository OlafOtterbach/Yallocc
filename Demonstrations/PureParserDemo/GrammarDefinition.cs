using LexSharp;
using Yallocc;

namespace PureParserDemo
{
   public static class GrammarDefinition
   {
      public static void DefineGrammar(this BuilderInterface<TokenType> builder)
      {
         // Expression
         //
         //                            |--plus---|            |-------------------plus----|
         //                            |         |            |                           |
         //                            |--minus--|            |-------------------minus---|
         //                            |        \|/          \|/                          |
         // --"ExpressionStart"-----------------------"ExpreesionLoop"------[Term]---------------->
         //
         builder.MasterGrammar("Expression")
             .Enter
             .Label("ExpressionStart")
             .Switch
              (
                builder.Branch.Token(TokenType.plus),
                builder.Branch.Token(TokenType.minus),
                builder.Branch.Default
              )
             .Label("ExpressionLoop")
             .Gosub("Term")
             .Switch
              (
                builder.Branch
                    .Token(TokenType.plus)
                    .Goto("ExpressionLoop"),
                builder.Branch
                    .Token(TokenType.minus)
                    .Goto("ExpressionLoop"),
                builder.Branch.Default
              )
             .Exit
             .EndGrammar();

         // Term
         //           |------------mult---------|
         //           |                         |
         //           |------------div----------|
         //          \|/                        |               
         // -----"TermStart"-->--[Factor]-------------->
         //
         builder.Grammar("Term")
             .Enter
             .Label("TermStart")
             .Gosub("Factor")
             .Switch
              (
                builder.Branch
                    .Token(TokenType.mult)
                    .Goto("TermStart"),
                builder.Branch
                    .Token(TokenType.div)
                    .Goto("TermStart"),
                builder.Branch.Default
              )
             .Exit
             .EndGrammar();

         // Factor
         //                                             
         //                      |---------number------->---|
         //                      |                          |
         // --"FactorStart"-->-------(-[Expression]-)--->- \|/----->
         //
         builder.Grammar("Factor")
             .Enter.Name("FactorStart")
             .Switch
              (
                 builder.Branch
                     .Token(TokenType.integer),
                 builder.Branch
                     .Token(TokenType.real),
                 builder.Branch
                     .Token(TokenType.open)
                     .Gosub("Expression")
                     .Token(TokenType.close)
               )
             .Exit
             .EndGrammar();
      }
   }
}
