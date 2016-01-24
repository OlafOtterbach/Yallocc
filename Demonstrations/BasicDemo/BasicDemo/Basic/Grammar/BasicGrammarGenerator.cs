using Yallocc.SyntaxTree;
using Yallocc.Tokenizer.LeTok;

namespace BasicDemo.Basic
{
   public class BasicGrammarGenerator
   {
      SyntaxTreeGenerator<TokenType> _generator;

      public BasicGrammarGenerator()
      {
         var tokenizerCreator = new LeTokCreator<TokenType>();
         _generator
         = SyntaxTreeGeneratorCreator<TokenType>.RegisterDefinitions(tokenizerCreator)
                                         .Register(new TokenDefinition())
                                         .Register(new ProgramGrammar())
                                         .Register(new ExpressionGrammar())
                                         .Register(new IfStatementGrammar())
                                         .Register(new WhileStatementGrammar())
                                         .Register(new ForStatementGrammar())
                                         .Register(new LetStatementGrammar())
                                         .Register(new DimStatementGrammar())
                                         .Register(new GotoStatementGrammar())
                                         .Register(new PlotStatementGrammar())
                                         .Register(new LabelStatementGrammar())
                                         .Create();
      }

      public SyntaxTreeResult<TokenType> Parse(string text)
      {
         return _generator.Parse(text);
      }
   }
}