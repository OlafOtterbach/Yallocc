using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LexSharp
{
   internal class TextCursor<T> where T : struct
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


      public Token<T> GetNextToken()
      {
         // Return buffer content if not empty
         if(!_buffer.IsEmpty)
         {
            return _buffer.Content;
         }

         var minimalPatternIndexAndLongestMininimalMatches = GetNextMatches();
         if (minimalPatternIndexAndLongestMininimalMatches.Any())
         { // Matches are found
            var tokens = minimalPatternIndexAndLongestMininimalMatches.Select(x => new Token<T>(x.Match.Value, x.Pattern.TokenType, x.Match.Index, x.Match.Length));
            var tokenResult = tokens.First();
            var actualCursorPos = _cursorPos;
            _cursorPos = tokenResult.TextIndex + ((tokenResult.Length > 0) ? tokenResult.Length : 1);
            ScanNextMatch();

            if (tokenResult.TextIndex == actualCursorPos)
            {
               return tokenResult;
            }
            else
            {
               var length = tokenResult.TextIndex - actualCursorPos;
               _buffer.Content = tokenResult;
               var untokenResult = new Token<T>(_text.Substring(actualCursorPos, length), actualCursorPos, length);
               return untokenResult;
            }

         }
         else
         { // No matches any more

            var length = _text.Length - _cursorPos;
            IsNotFinished = length > 0;

            if (IsNotFinished)
            {  // Any text to return at the end?
               var restResult = new Token<T>(_text.Substring(_cursorPos, length), _cursorPos, length);
               _cursorPos = _text.Length;
               return restResult;
            }
            else
            { // No token any more
               var emptyToken = new Token<T>();
               return emptyToken;
            }
         }
      }

      private IEnumerable<PatternAndMatch> GetNextMatches()
      {
         _matches = _matches.Where(x => x.Match.Success).Where(m => m.Match.Index >= _cursorPos).ToList();
         var minIndex = _matches.Any() ? _matches.Min(x => x.Match.Index) : 0;
         var minMatches = _matches.Where(x => x.Match.Index == minIndex);
         var maxLength = minMatches.Any() ? minMatches.Max(x => x.Match.Length) : 0;
         var longestMinimalMatches = minMatches.Where(x => x.Match.Length == maxLength);
         var minPatternIndex = longestMinimalMatches.Any() ? longestMinimalMatches.Min(x => x.PatternIndex) : 0;
         var minimalPatternIndexAndLongestMininimalMatches = longestMinimalMatches.Where(x => x.PatternIndex == minPatternIndex);
         return minimalPatternIndexAndLongestMininimalMatches;
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
