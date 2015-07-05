﻿using LexSharp;
using SyntaxTree;
using Yallocc;

namespace YalloccDemo.Grammar
{
   public class BasicGrammarGenerator
   {
      public YParser<TokenType> CreateParser(SyntaxTreeBuilder stb)
      {
         var yacc = new Yallocc<TokenType>();

         var tokenDefinition = new TokenDefinition();
         tokenDefinition.DefineExpressionTokens(yacc);
         var expressionGrammar = new ExpressionGrammar();
         expressionGrammar.DefineGrammar(yacc, stb);

         var parser = yacc.CreateParser();
         return parser;
      }
   }
}