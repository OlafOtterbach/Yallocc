﻿using SyntaxTree;
using Yallocc;

namespace SyntaxTreeDemo
{
   public class TokenDefinition : ITokenAndGrammarDefinition<TokenType>
   {
      public void Define(Yallocc<SyntaxTreeBuilder, TokenType> yacc)
      {
         yacc.DefineTokens()
             .AddTokenPattern(@"\+", TokenType.plus)
             .AddTokenPattern(@"\-", TokenType.minus)
             .AddTokenPattern(@"\*", TokenType.mult)
             .AddTokenPattern(@"\/", TokenType.div)
             .AddTokenPattern(@"\(", TokenType.open)
             .AddTokenPattern(@"\)", TokenType.close)
             .AddTokenPattern(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer)
             .AddTokenPattern(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real)
             .AddIgnorePattern(@"( |\t)+", TokenType.white_space)
             .End();
      }
   }
}