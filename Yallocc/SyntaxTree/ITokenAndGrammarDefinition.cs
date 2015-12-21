using Yallocc;

namespace Yallocc.SyntaxTree
{
   public interface ITokenAndGrammarDefinition<T> where T : struct
   {
      void Define(Yallocc<SyntaxTreeBuilder, T> yacc);
   }
}
