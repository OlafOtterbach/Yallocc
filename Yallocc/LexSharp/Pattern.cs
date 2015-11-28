using System.Text.RegularExpressions;

namespace LexSharp
{
   public struct Pattern<T>
   {
      public Pattern(string pattern, T tokenType) : this()
      {
         TokenPattern = pattern != null ? new Regex(pattern, RegexOptions.CultureInvariant) : null;
         TokenType = tokenType;
      }

      public Regex TokenPattern { get; private set; }

      public T TokenType { get; private set; }
   }
}
