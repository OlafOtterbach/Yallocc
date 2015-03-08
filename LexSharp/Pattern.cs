using System.Text.RegularExpressions;

namespace LexSharp
{
   public struct Pattern
   {
      public Pattern(string pattern, ITokenType tokenType) : this()
      {
         TokenPattern = new Regex(pattern, RegexOptions.Compiled);
         TokenType = tokenType;
      }

      public Regex TokenPattern { get; private set; }

      public ITokenType TokenType { get; private set; }
   }
}
