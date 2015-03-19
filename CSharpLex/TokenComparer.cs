using System;
using System.Collections.Generic;

namespace LexSharp
{
   internal class TokenComparer<T> : IComparer<Token<T>>
   {
      public TokenComparer(Dictionary<int, int> patternTypeIndexMap)
      {
         _patternTypeIndexMap = patternTypeIndexMap;
      }

      public int Compare(Token<T> x, Token<T> y)
      {
         var compared = IsLess(x, y) ? -1 : IsGreater(x, y) ? 1 : 0;
         return compared;
      }

      private bool IsLess(Token<T> x, Token<T> y)
      {
         return (x.TextIndex < y.TextIndex) || ((x.TextIndex == y.TextIndex) && (x.Length > y.Length) ) || ((x.TextIndex == y.TextIndex) && (x.Length == y.Length) && (_patternTypeIndexMap[x.Type.GetHashCode()] < (_patternTypeIndexMap[y.Type.GetHashCode()])));
      }

      private bool IsGreater(Token<T> x, Token<T> y)
      {
         return (x.TextIndex > y.TextIndex) || ((x.TextIndex == y.TextIndex) && (x.Length < y.Length)) || ((x.TextIndex == y.TextIndex) && (x.Length == y.Length) && (_patternTypeIndexMap[x.Type.GetHashCode()] > (_patternTypeIndexMap[y.Type.GetHashCode()])));
      }

      Dictionary<int, int> _patternTypeIndexMap;
   }
}
