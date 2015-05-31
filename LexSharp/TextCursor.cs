using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LexSharp
{
   internal class TextCursor<T>
   {
      private class PatternAndMatch
      {
         public int Index { get; set; }
         public Pattern<T> Pattern { get; set; }
         public Match Match { get; set; }
      }

      private IEnumerable<PatternAndMatch> _matches;

      private int _cursorPos;

      public TextCursor(string text, IEnumerable<Pattern<T>> patterns)
      {
         _matches = patterns.Select((p, i) => new PatternAndMatch { Index = i, Pattern = p, Match = p.TokenPattern.Match(text) });
         _cursorPos = 0;
      }

      public TokenResult<T> GetNextToken()
      {
         _matches = _matches.Where(x => x.Match.Success).Where(m => m.Match.Index >= _cursorPos).ToList();
         var minIndex = _matches.Any() ? _matches.Min(x => x.Match.Index) : 0;
         var minMatches = _matches.Where(x => x.Match.Index == minIndex);
         var maxLength = minMatches.Any() ? minMatches.Max(x => x.Match.Length) : 0;
         var longestMinimalMatches = minMatches.Where(x => x.Match.Length == maxLength);
         var minPatternIndex = longestMinimalMatches.Any() ? longestMinimalMatches.Min(x => x.Index) : 0;
         var minimalPatternIndexAndLongestMininimalMatches = longestMinimalMatches.Where(x => x.Index == minPatternIndex);
         var result = new TokenResult<T>() { Token = minimalPatternIndexAndLongestMininimalMatches.Select(x => new Token<T>(x.Match.Value, x.Pattern.TokenType, x.Match.Index, x.Match.Length)).FirstOrDefault(), IsValid = minimalPatternIndexAndLongestMininimalMatches.Count() > 0 };
         if( result.IsValid)
         {
            _cursorPos = result.Token.TextIndex + ((result.Token.Length > 0) ? result.Token.Length : 1);
            ScanNextMatch();
         }
         return result;
      }

      private void ScanNextMatch()
      {
         foreach (var match in _matches)
         {
            if (match.Match.Index < _cursorPos)
            {
               match.Match = match.Match.NextMatch();
            }
         }
      }
   }
}
