using System.Collections.Generic;

namespace LexSharp
{
   public struct ScanResult<T>
   {
      public IEnumerable<Token<T>> TokenSequence { get; set; }

      //public IEnumerable<Token<T>> UnknownTokens { get; set; }

      public bool Success { get; set; }
   }
}
