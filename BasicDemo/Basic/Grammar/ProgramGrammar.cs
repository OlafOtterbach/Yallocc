using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class ProgramGrammar
   {
      public void DefineGrammar(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.MasterGrammar("Program")
             .Enter
             .Gosub("StatementSequence")
             .Exit
             .EndGrammar();

         yacc.Grammar("StatementSequence")
             .Enter.Action(() => stb.Enter())
             .Label("NextStatement")
             .Gosub("Statement")
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.Return)
                     .Goto("NextStatement"),
                 yacc.Branch.Default
              )
             .Token(TokenType.Return)
             .Exit.Action(() => stb.Exit())
             .EndGrammar();

         yacc.Grammar("Statement")
             .Enter.Action(() => stb.Enter())
             .Switch
              (
                 yacc.Branch
                     .Gosub("LetStatement"),
                 yacc.Branch.Default
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}