using Yallocc.SyntaxTree;
using Yallocc;
using System;
using Yallocc.Tokenizer;

namespace Yallocc.SyntaxTreeTest.SimpleGrammar
{
   public class SimpleGrammarDefinition : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         yacc.MasterGrammar("Master")
               .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
               .Lambda.Action((SyntaxTreeBuilder stb) => stb.CreateParent(new SyntaxTreeNode()))
               .Gosub("Intermediate").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
               .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
               .EndGrammar();

         yacc.Grammar("Intermediate")
               .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
               .Gosub("Bottom").Action((SyntaxTreeBuilder stb) => stb.AdoptInnerNodes())
               .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
               .EndGrammar();

         yacc.Grammar("Bottom")
               .Enter.Action((SyntaxTreeBuilder stb) => stb.Enter())
               .Token(TokenType.A).Action((SyntaxTreeBuilder stb, Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
               .Token(TokenType.B).Action((SyntaxTreeBuilder stb, Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
               .Token(TokenType.C).Action((SyntaxTreeBuilder stb, Token<TokenType> tok) => stb.AddChild(new TokenTreeNode<TokenType>(tok)))
               .Exit.Action((SyntaxTreeBuilder stb) => stb.Exit())
               .EndGrammar();
      }
   }
}