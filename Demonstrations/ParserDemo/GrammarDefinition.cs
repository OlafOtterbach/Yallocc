using Yallocc;

namespace ParserDemo
{
   public static class GrammarDefinition
   {
      public static void DefineParserGrammar(this Yallocc<DummyContext, TokenType> yacc)
      {
         // Expression
         //
         //                            |--plus---|            |-------------------plus----|
         //                            |         |            |                           |
         //                            |--minus--|            |-------------------minus---|
         //                            |        \|/          \|/                          |
         // --"ExpressionStart"-----------------------"ExpreesionLoop"------[Term]---------------->
         //
         yacc.MasterGrammar("Expression")
             .Enter
             .Label("ExpressionStart")
             .Switch
              (
                yacc.Branch.Token(TokenType.plus),
                yacc.Branch.Token(TokenType.minus),
                yacc.Branch.Default
              )
             .Label("ExpressionLoop")
             .Gosub("Term")
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.plus)
                    .Goto("ExpressionLoop"),
                yacc.Branch
                    .Token(TokenType.minus)
                    .Goto("ExpressionLoop"),
                yacc.Branch.Default
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
         yacc.Grammar("Term")
             .Enter
             .Label("TermStart")
             .Gosub("Factor")
             .Switch
              (
                yacc.Branch
                    .Token(TokenType.mult)
                    .Goto("TermStart"),
                yacc.Branch
                    .Token(TokenType.div)
                    .Goto("TermStart"),
                yacc.Branch.Default
              )
             .Exit
             .EndGrammar();

         // Factor
         //                                             
         //                      |---------number------->---|
         //                      |                          |
         // --"FactorStart"-->-------(-[Expression]-)--->- \|/----->
         //
         yacc.Grammar("Factor")
             .Enter.Name("FactorStart")
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.integer),
                 yacc.Branch
                     .Token(TokenType.real),
                 yacc.Branch
                     .Token(TokenType.open)
                     .Gosub("Expression")
                     .Token(TokenType.close)
               )
             .Exit
             .EndGrammar();
      }
   }
}
