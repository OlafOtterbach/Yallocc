using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LexSharp
{
   internal class TextCursor<T>
   {
      private class PatternAndMatch
      {
         public int PatternIndex { get; set; }
         public Pattern<T> Pattern { get; set; }
         public Match Match { get; set; }
      }

      private IEnumerable<PatternAndMatch> _matches;

      private int _cursorPos;

      private string _text;

      private TokenResultBuffer<T> _buffer;

      public TextCursor(string text, IEnumerable<Pattern<T>> patterns)
      {
         _text = text;
         _matches = patterns.Select((p, i) => new PatternAndMatch { PatternIndex = i, Pattern = p, Match = p.TokenPattern.Match(text) });
         _cursorPos = 0;
         _buffer = new TokenResultBuffer<T>();
         IsNotFinished = true;
      }

      public bool IsNotFinished { get; private set; }

      public TokenResult<T> GetNextToken()
      {
         if(!_buffer.IsEmpty)
         {
            return _buffer.Content;
         }
         _matches = _matches.Where(x => x.Match.Success).Where(m => m.Match.Index >= _cursorPos).ToList();
         var minIndex = _matches.Any() ? _matches.Min(x => x.Match.Index) : 0;
         var minMatches = _matches.Where(x => x.Match.Index == minIndex);
         var maxLength = minMatches.Any() ? minMatches.Max(x => x.Match.Length) : 0;
         var longestMinimalMatches = minMatches.Where(x => x.Match.Length == maxLength);
         var minPatternIndex = longestMinimalMatches.Any() ? longestMinimalMatches.Min(x => x.PatternIndex) : 0;
         var minimalPatternIndexAndLongestMininimalMatches = longestMinimalMatches.Where(x => x.PatternIndex == minPatternIndex);
         var result = new TokenResult<T>() { Token = minimalPatternIndexAndLongestMininimalMatches.Select(x => new Token<T>(x.Match.Value, x.Pattern.TokenType, x.Match.Index, x.Match.Length)).FirstOrDefault(), IsValid = minimalPatternIndexAndLongestMininimalMatches.Count() > 0 };
         if( result.IsValid)
         {
            var cursorPos = _cursorPos;
            _cursorPos = result.Token.TextIndex + ((result.Token.Length > 0) ? result.Token.Length : 1);
            if (result.Token.TextIndex > cursorPos)
            {
               var length = result.Token.TextIndex - cursorPos;
               _buffer.Content = result;
               result = new TokenResult<T>()
               {
                  Token = new Token<T>(_text.Substring(cursorPos, length),default(T),cursorPos,length),
                  IsValid = false
               };
            }
            ScanNextMatch();
         }
         else
         {
            var length = _text.Length - _cursorPos;
            if (length > 0)
            {
               result = new TokenResult<T>()
               {
                  Token = new Token<T>(_text.Substring(_cursorPos, length), default(T), _cursorPos, length),
                  IsValid = false
               };
               _cursorPos = _text.Length;
            }
            IsNotFinished = length > 0;
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
