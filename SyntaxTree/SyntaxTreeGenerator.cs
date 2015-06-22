using Yallocc;

namespace SyntaxTree
{
   public class SyntaxTreeGenerator<T> where T : struct
   {
      private Yallocc<T> _yacc;
      private SyntaxTreeBuilder _syntaxTreeBuilder;
      private YParser<T> _parser;

      public SyntaxTreeGenerator(Yallocc<T> yacc, SyntaxTreeBuilder syntaxTreeBuilder)
      {
         _yacc = yacc;
         _syntaxTreeBuilder = syntaxTreeBuilder;
         _parser = _yacc.CreateParser();
      }

      public SyntaxTreeBuilderResult Parse(string text)
      {
         _syntaxTreeBuilder.Reset();
         var parseResult = _parser.Parse(text);
         var result = new SyntaxTreeBuilderResult(_syntaxTreeBuilder.Root, parseResult);
         return result;
      }
   }
}
