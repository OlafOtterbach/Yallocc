using Yallocc.Tokenizer;
using Yallocc.SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class ProgramGrammar : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         yacc.MasterGrammar("Program")
             .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Token(TokenType.program_keyword).Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.CreateParent(new TokenTreeNode<TokenType>(tok)))
             .Token(TokenType.text).Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Token(TokenType.Return)
             .Gosub("StatementSequence").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Token(TokenType.end_keyword).Action((SyntaxTreeBuilder stb,Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();

         yacc.Grammar("StatementSequence")
             .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Label("NextStatement")
             .Gosub("Statement").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
             .Switch
              (
                 yacc.Branch
                     .Token(TokenType.Return)
                     .Goto("NextStatement"),
                 yacc.Branch.Default
              )
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();

         yacc.Grammar("Statement")
             .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
             .Switch
              (
                 yacc.Branch
                     .Gosub("IfStatement").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch
                     .Gosub("WhileStatement").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch
                     .Gosub("ForStatement").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch
                     .Gosub("LetStatement").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch
                     .Gosub("DimStatement").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch
                     .Gosub("GotoStatement").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch
                     .Gosub("PlotStatement").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch
                     .Gosub("LabelStatement").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes()),
                 yacc.Branch.Default
              )
             .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
             .EndGrammar();
      }
   }
}