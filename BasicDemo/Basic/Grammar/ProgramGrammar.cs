using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class ProgramGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<TokenType> yacc, SyntaxTreeBuilder stb)
      {
         yacc.MasterGrammar("Program")
             .Enter.Action(() => stb.Enter())
             .Token(TokenType.program).Action((Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode(tok)))
             .Token(TokenType.text).Action((Token<TokenType> tok) => stb.AddChild(new TokenTreeNode(tok)))
             .Token(TokenType.Return)
             .Gosub("StatementSequence").Action(() => stb.AdoptInnerNodes())
             .Exit.Action(() => stb.Exit())
             .EndGrammar();

         yacc.Grammar("StatementSequence")
             .Enter.Action(() => stb.Enter())
             .Label("NextStatement")
             .Gosub("Statement").Action(() => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.Return)
                     .Goto("NextStatement"),
                 yacc.Branch.Default
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();

         yacc.Grammar("Statement")
             .Enter.Action(() => stb.Enter())
             .Switch
              (
                 yacc.Branch
                     .Gosub("LetStatement").Action(() => stb.AdoptInnerNodes()),
                 yacc.Branch
                     .Gosub("DimStatement").Action(() => stb.AdoptInnerNodes()),
                 yacc.Branch.Default
              )
             .Exit.Action(() => stb.Exit())
             .EndGrammar();
      }
   }
}