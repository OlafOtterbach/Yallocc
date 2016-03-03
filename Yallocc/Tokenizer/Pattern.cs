/// <summary>Tokenizer</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

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

      public Regex TokenPattern { get; }

      public T TokenType { get; }
   }
}
