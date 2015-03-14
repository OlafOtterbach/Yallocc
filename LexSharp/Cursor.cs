using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LexSharp
{
   internal class Cursor
   {
      private class PatternAndMatch
      {
         public int Index { get; set; }
         public Pattern Pattern { get; set; }
         public Match Match { get; set; }
      }

      IEnumerable<PatternAndMatch> _matches;
      private int _scanPos;

      public Cursor(string text, IEnumerable<Pattern> patterns)
      {
         _matches = patterns.Select((p, i) => new PatternAndMatch { Index = i, Pattern = p, Match = p.TokenPattern.Match(text) });
         _scanPos = 0;
      }

      public ScanResult GetNextToken()
      {
         _matches = _matches.Where(x => x.Match.Success).Where(m => m.Match.Index >= _scanPos).ToList();
         var minIndex = _matches.Min(x => x.Match.Index);
         var minMatches = _matches.Where(x => x.Match.Index == minIndex);
         var maxLength = minMatches.Max(x => x.Match.Length);
         var longestMinimalMatches = minMatches.Where(x => x.Match.Length == maxLength);
         var minPatternIndex = longestMinimalMatches.Min(x => x.Index);
         var minimalPatternIndexAndLongestMininimalMatches = longestMinimalMatches.Where(x => x.Index == minPatternIndex);
         var result = new ScanResult() { Token = minimalPatternIndexAndLongestMininimalMatches.Select(x => new Token(x.Match.Value, x.Pattern.TokenType, x.Match.Index, x.Match.Length)).FirstOrDefault(), IsValid = minimalPatternIndexAndLongestMininimalMatches.Count() > 0 };
         if( result.IsValid)
         {
            _scanPos += result.Token.Length;
            ScanNextMatch();
         }
         return result;
      }

      private void ScanNextMatch()
      {
         foreach (var match in _matches)
         {
            if (match.Match.Index < _scanPos)
            {
               match.Match = match.Match.NextMatch();
            }
         }
      }
   }
}
