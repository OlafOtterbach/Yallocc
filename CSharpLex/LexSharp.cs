using System.Collections.Generic;
using System.Linq;

namespace LexSharp
{
   public class LexSharp<T>
   {
      private List<Pattern<T>> _patterns;

      public LexSharp()
      {
         _patterns = new List<Pattern<T>>();
      }

      public void Register(string patternText, T tokenType)
      {
         var pattern = new Pattern<T>(patternText, tokenType);
         _patterns.Add(pattern);
      }

      public IEnumerable<Token<T>> Scan(string text)
      {
         var TextCursor = new TextCursor<T>(text, _patterns);
         var tokens = Enumerable.Range(0, int.MaxValue)
                                .Select(x => TextCursor.GetNextToken())
                                .TakeWhile(r => r.IsValid)
                                .Select(t => t.Token)
                                .ToList();
         return tokens;
      }
   }
}
