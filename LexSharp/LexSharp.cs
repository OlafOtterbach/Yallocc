using System;
using System.Linq;
using System.Collections.Generic;

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
         if(_patterns.Any(p => p.TokenType.Equals(tokenType)))
         {
            throw new TokenRegisteredMoreThanOneTimeException<T>(tokenType, "Not allowed to register Token more than one time");
         }
         _patterns.Add(pattern);
      }

      public IEnumerable<TokenResult<T>> Scan(string text)
      {
         var TextCursor = new TextCursor<T>(text, _patterns);
         var tokens = Enumerable.Range(0, int.MaxValue)
                                .Select(x => TextCursor.GetNextToken())
                                .TakeWhile(r => TextCursor.IsNotFinished);
         return tokens;
      }

      public bool IsComplete()
      {
         var isComplete = Enum.GetValues(typeof(T)).OfType<T>().All(tokType => _patterns.Any(x => x.TokenType.Equals(tokType)));
         return isComplete;
      }
   }
}
