using System.Text.RegularExpressions;

namespace Yallocc.Tokenizer
{
   public struct Pattern<T>
   {
      public Pattern(Regex pattern, T tokenType) : this()
      {
         TokenPattern = pattern;
         TokenType = tokenType;
      }

      public Regex TokenPattern { get; private set; }

      public T TokenType { get; private set; }
   }
}
