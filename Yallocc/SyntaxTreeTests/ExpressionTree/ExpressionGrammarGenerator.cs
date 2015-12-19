using SyntaxTree;
using Yallocc;
using Yallocc.Tokenizer.LeTok;

namespace SyntaxTreeTest.ExpressionTree
{
   public class ExpressionGrammarGenerator
   {
      public SyntaxTreeGenerator<ExpressionTokenType> CreateParser()
      {
         var generator
             = SyntaxTreeGeneratorCreator<ExpressionTokenType>.Make(new LeTokCreator<ExpressionTokenType>())
                                                       .Register(new ExpressionTokenDefinition())
                                                       .Register(new ExpressionGrammarDefinition())
                                                       .Create();
         return generator;
      }
   }
}