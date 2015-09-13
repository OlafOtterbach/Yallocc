using LexSharp;
using SyntaxTree;
using Yallocc;

namespace BasicDemo.Basic
{
   public class BasicGrammarGenerator
   {
      SyntaxTreeGenerator<TokenType> _generator;

      public BasicGrammarGenerator()
      {
         _generator
         = SyntaxTreeGenerator<TokenType>.Make
                                         .Register(new TokenDefinition())
                                         .Register(new ProgramGrammar())
                                         .Register(new ExpressionGrammar())
                                         .Register(new LetStatementGrammar())
                                         .Register(new DimStatementGrammar())
                                         .Register(new GotoStatementGrammar())
                                         .Register(new LabelStatementGrammar())
                                         .Create();
      }

      public SyntaxTreeBuilderResult Parse(string text)
      {
         return _generator.Parse(text);
      }
   }
}