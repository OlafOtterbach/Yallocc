using Yallocc;

namespace SyntaxTree
{
   public interface ITokenAndGrammarDefinition<T> where T : struct
   {
      void Define(Yallocc<T> yacc, SyntaxTreeBuilder stb);
   }
}
