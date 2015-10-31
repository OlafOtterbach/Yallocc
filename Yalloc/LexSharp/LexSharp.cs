using System;
using System.Linq;
using System.Collections.Generic;

namespace LexSharp
{
   public class LexSharp<T> where T : struct
   {
      private List<Pattern<T>> _patterns;
      private List<T> _ignoreTokenType;

      public LexSharp()
      {
         _patterns = new List<Pattern<T>>();
         _ignoreTokenType = new List<T>();
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

      public void RegisterIgnorePattern(string patternText, T tokenType)
      {
         Register(patternText, tokenType);
         _ignoreTokenType.Add(tokenType);
      }

      public IEnumerable<Token<T>> Scan(string text)
      {
         var TextCursor = new TextCursor<T>(text, _patterns);
         var tokens = Enumerable.Range(0, int.MaxValue)
                                .Select(x => TextCursor.GetNextToken())
                                .TakeWhile(r => TextCursor.IsNotFinished);
         var validTokens = tokens.Where( tok => (tok.Type == null) || (!_ignoreTokenType.Contains((T)tok.Type)));
         return validTokens;
      }

      public bool IsComplete()
      {
         var isComplete = Enum.GetValues(typeof(T)).OfType<T>().All(tokType => _patterns.Any(x => x.TokenType.Equals(tokType)));
         return isComplete;
      }
   }
}
