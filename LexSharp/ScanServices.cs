using System.Linq;
using System.Collections.Generic;

namespace LexSharp
{
   public static class ScanServices
   {
      struct Interval
      {
         int Start;
         int End;
      }

      public static void ValidateScanResult<T>(IEnumerable<Token<T>> sequence, string text )
      {

      }
   }
}
