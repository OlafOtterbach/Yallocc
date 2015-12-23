﻿using System.Collections.Generic;
using System.Linq;

namespace Yallocc.Tokenizer
{
   public abstract class Tokenizer<T> where T : struct
   {
      public Tokenizer(IEnumerable<Pattern<T>> patterns, IEnumerable<Pattern<T>> patternsToIgnore)
      {
         Patterns = patterns.Concat(patternsToIgnore).ToArray();
         IgnoreTokenType = patternsToIgnore.Select(p => p.TokenType).ToArray();
      }

      public abstract IEnumerable<Token<T>> Scan(string text);

      protected Pattern<T>[] Patterns { get; }

      protected T[] IgnoreTokenType { get; }
   }
}
