using System.Collections.Generic;

namespace LexSharp
{
   public interface ITokenizer<T> where T : struct
   {
      void Register(string patternText, T tokenType);
      void RegisterIgnorePattern(string patternText, T tokenType);
      void Initialize();
      bool IsComplete();
      IEnumerable<Token<T>> Scan(string text);
   }
}
