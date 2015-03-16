﻿using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CSharpLex
{
   public class Lex
   {
      public Lex()
      {
         _patterns = new List<Pattern>();
         _patternTypeIndexMap = new Dictionary<int, int>();
      }

      public void Register(string patternText, ITokenType tokenType)
      {
         var pattern = new Pattern(patternText, tokenType);
         _patterns.Add(pattern);
         _patternTypeIndexMap.Add(pattern.TokenType.GetHashCode(), _patterns.Count - 1);
      }

      public IEnumerable<Token> Scan(string text)
      {
         var matchesOfPatterns = _patterns.AsParallel()
                                          .SelectMany(p => p.TokenPattern
                                                        .Matches(text)
                                                        .Cast<Match>()
                                                        .Select(m => new Token(m.Value, p.TokenType, m.Index, m.Length)))
                                          .OrderBy(token => token, new TokenComparer(_patternTypeIndexMap)).ToList();
         var tokens = new List<Token>();
         Token actual = new Token(string.Empty,null,-1,0);
         foreach (var token in matchesOfPatterns)
         {
            if(actual.TextIndex + actual.Length - 1 < token.TextIndex)
            {
               actual = token;
               tokens.Add(token);
            }
         }
         return tokens;
      }

      public IEnumerable<Token> Scan2(string text)
      {
         var cursor = new Cursor(text, _patterns);
         var tokens = Enumerable.Range(0, int.MaxValue)
                                .Select(x => cursor.GetNextToken())
                                .TakeWhile(r => r.IsValid)
                                .Select(t => t.Token)
                                .ToList();
         return tokens;
      }


      private List<Pattern> _patterns;

      private Dictionary<int, int> _patternTypeIndexMap;
   }
}
