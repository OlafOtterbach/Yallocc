/// <summary>LeTok</summary>
/// <summary>[Le]exical [Tok]enizer.</summary>
/// <author>Olaf Otterbach</author>
/// <date>11.11.2015</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Yallocc.Tokenizer.LeTok
{
   public class LeTok<T> : Tokenizer<T> where T : struct
   {
      private struct MatchPair
      {
         public Match Match;
         public int Index;
         public bool IsValid;
      }

      private readonly Regex _regex;
      private readonly int _skipUnnamedGroups;


      public LeTok(IEnumerable<Pattern<T>> patterns, IEnumerable<Pattern<T>> patternsToIgnor) : base(patterns, patternsToIgnor)
      {
         var patternForAll = Patterns.Where(pattern => pattern.TokenPattern != null)
                                      .Select(x => string.Format("(?<{0}>{1})", "M" + x.TokenType, x.TokenPattern))
                                      .Aggregate((current, elem) => current + "|" + elem);
         _regex = new Regex(patternForAll, RegexOptions.Compiled | RegexOptions.CultureInvariant);

         var groupNames = _regex.GetGroupNames();
         var patternNames = Patterns.Select(p => "M" + p.TokenType.ToString());
         _skipUnnamedGroups = groupNames.Count(x => !patternNames.Contains(x));
      }


      public override IEnumerable<Token<T>> Scan(string text)
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
         var validTokens = tokens.Where(tok => (tok.Type == null) || (!IgnoreTokenType.Contains((T)tok.Type)));

         return validTokens;
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
         var token = new Token<T>(match.Match.Value, Patterns[match.Index].TokenType, match.Match.Index, match.Match.Length);
         return token;
      }


      private Token<T> CreateDefaultToken(string text, int start, int length)
      {
         var untoken = new Token<T>(text.Substring(start, length), start, length);
         return untoken;
      }
   }
}
