using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LexSharp
{
   public class LeTok<T> where T : struct
   {
      private List<Pattern<T>> _patterns;
      private List<T> _ignoreTokenType;
      private string _patternForAll;

      public LeTok()
      {
         _patterns = new List<Pattern<T>>();
         _ignoreTokenType = new List<T>();
      }

      public void Register(string patternText, T tokenType)
      {
         var pattern = new Pattern<T>(patternText, tokenType);
         if (_patterns.Any(p => p.TokenType.Equals(tokenType)))
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

      public void Init()
      {
         _patternForAll = _patterns.Select(x => string.Format("(?<{0}>{1})", "M" + x.TokenType, x.TokenPattern))
                                   .Aggregate((current, elem) => current + "|" + elem);
      }

      private struct MatchPair
      {
         public Match Match;
         public int Index;
         public bool IsValid;
      }

      public IEnumerable<Token<T>> Scan(string text)
      {
         var matches = Regex.Matches(text, _patternForAll)
                            .Cast<Match>()
                            .SelectMany(m => m.Groups.Cast<Match>()
                                                     .Select((x, i) => new MatchPair { Match = x, Index = i, IsValid = true })
                            .Where(p => p.Match.Success).Take(1));

         var none = new List<MatchPair> { new MatchPair { IsValid = false } };

         var sequence = none.Concat(matches.Reverse().Skip(1).Reverse())
                            .Zip(matches, (prev, curr) => Create(text, prev, curr))
                            .Aggregate((current, elem) => current = current.Concat(elem));
         return sequence;
      }

      private IEnumerable<Token<T>> Create(string text, MatchPair prev, MatchPair curr)
      {
         if (prev.IsValid)
         {
            var delta = curr.Match.Index - prev.Match.Index - prev.Match.Length;
            if (delta > 0)
            {
               yield return CreateDefaultToken(text, prev.Match.Index + delta - 1, delta);
            }
         }
         yield return CreateToken(curr);
      }

      private Token<T> CreateToken(MatchPair match)
      {
         var token = new Token<T>(match.Match.Value, _patterns[match.Index].TokenType, match.Match.Index, match.Match.Length);
         return token;
      }


      private Token<T> CreateDefaultToken(string text, int start, int length)
      {
         var untoken = new Token<T>(text.Substring(start, length), start, length);
         return untoken;
      }
   }
}
