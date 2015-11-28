using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LexSharp
{
   public class LeTok<T> : ITokenizer<T> where T : struct
   {
      private struct MatchPair
      {
         public Match Match;
         public int Index;
         public bool IsValid;
      }

      private List<Pattern<T>> _patterns;

      private List<T> _ignoreTokenType;

      private Regex _regex;

      private int _skipUnnamedGroups;

      internal LeTok()
      {
         _patterns = new List<Pattern<T>>();
         _ignoreTokenType = new List<T>();
         _skipUnnamedGroups = 0;
      }

      public void Register(string patternText, T tokenType)
      {
         if(patternText == null)
         {
            throw new ArgumentNullException("patternText");
         }

         var pattern = new Pattern<T>();
         try
         {
             pattern = new Pattern<T>(patternText, tokenType);
         }
         catch (ArgumentException)
         {
            throw new ArgumentException("patterntext {0} is not a regular expression.", patternText);
         };

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

      public void Initialize()
      {
         var patternForAll = _patterns.Where(pattern => pattern.TokenPattern != null)
                                      .Select(x => string.Format("(?<{0}>{1})", "M" + x.TokenType, x.TokenPattern))
                                      .Aggregate((current, elem) => current + "|" + elem);
         _regex = new Regex(patternForAll, RegexOptions.Compiled | RegexOptions.CultureInvariant);

         var groupNames = _regex.GetGroupNames();
         var patternNames = _patterns.Select(p => "M" + p.TokenType.ToString());
         _skipUnnamedGroups = groupNames.Count(x => !patternNames.Contains(x));
      }

      public IEnumerable<Token<T>> Scan(string text)
      {
         if(text == null)
         {
            return new List<Token<T>>();
         }

         var matches = _regex.Matches(text)
                             .OfType<Match>()
                             .SelectMany(m => m.Groups.Cast<Group>()
                                                      .Skip(_skipUnnamedGroups)
                                                      .Select((x, i) => new MatchPair { Match = m, Index = i, IsValid = x.Success })
                             .Where(p => p.IsValid).Take(1));

         var none = new List<MatchPair> { new MatchPair { IsValid = false } };

         var tokens = none.Concat(matches)
                          .Zip(matches.Concat(none), (prev, curr) => Create(text, prev, curr))
                          .SelectMany(x => x);
         var validTokens = tokens.Where(tok => (tok.Type == null) || (!_ignoreTokenType.Contains((T)tok.Type)));

         return validTokens;
      }

      public bool IsComplete()
      {
         if (!_patterns.Any())
         {
            return false;
         }

         bool isEnum = _patterns.First().TokenType is Enum;
         if (isEnum)
         {
            var isComplete = Enum.GetValues(typeof(T)).OfType<T>().All(tokType => _patterns.Any(x => x.TokenType.Equals(tokType)));
            return isComplete;
         }
         else
         {
            throw new TokenIsNotAnEnumTypeException("Can not test on completeness. Type is not enum type");
         }
      }

      private IEnumerable<Token<T>> Create(string text, MatchPair prev, MatchPair curr)
      {
         if(prev.IsValid)
         {
            if(curr.IsValid)
            {
               var delta = curr.Match.Index - prev.Match.Index - prev.Match.Length;
               if (delta > 0)
               {
                  yield return CreateDefaultToken(text, prev.Match.Index + delta - 1, delta);
               }
               yield return CreateToken(curr);
            }
            else
            {
               var delta = text.Length - prev.Match.Index - prev.Match.Length;
               if (delta > 0)
               {
                  yield return CreateDefaultToken(text, prev.Match.Index + delta - 1, delta);
               }
            }
         }
         else
         {
            if(curr.IsValid)
            {
               var delta = curr.Match.Index;
               if (delta > 0)
               {
                  yield return CreateDefaultToken(text, 0, delta);
               }
               yield return CreateToken(curr);
            }
            else
            {
               if (text.Length > 0)
               {
                  yield return CreateDefaultToken(text, 0, text.Length);
               }
            }
         }
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
