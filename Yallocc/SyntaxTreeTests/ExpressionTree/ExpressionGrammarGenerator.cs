using LexSharp;
using SyntaxTree;
using Yallocc;

namespace SyntaxTreeTest.ExpressionTree
{
   public class ExpressionGrammarGenerator
   {
      public SyntaxTreeGenerator<ExpressionTokenType> CreateParser()
      {
         var generator
             = SyntaxTreeGenerator<ExpressionTokenType>.Make
                                                       .Register(new ExpressionTokenDefinition())
                                                       .Register(new ExpressionGrammarDefinition())
                                                       .Create();
         return generator;
      }
   }
}