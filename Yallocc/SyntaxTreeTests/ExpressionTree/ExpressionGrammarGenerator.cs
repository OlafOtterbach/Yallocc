using Yallocc.SyntaxTree;
using Yallocc;
using Yallocc.Tokenizer.LeTok;

namespace Yallocc.SyntaxTreeTest.ExpressionTree
{
   public class ExpressionGrammarGenerator
   {
      public SyntaxTreeGenerator<ExpressionTokenType> CreateParser()
      {
         var generator
             = SyntaxTreeGeneratorCreator<ExpressionTokenType>.RegisterDefinitions(new LeTokCreator<ExpressionTokenType>())
                                                       .Register(new ExpressionTokenDefinition())
                                                       .Register(new ExpressionGrammarDefinition())
                                                       .Create();
         return generator;
      }
   }
}