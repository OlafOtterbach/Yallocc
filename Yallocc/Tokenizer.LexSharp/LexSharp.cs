﻿/// <summary>Tokenizer</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

using System.Collections.Generic;
using System.Linq;

namespace Yallocc.Tokenizer.LexSharp
{
   public class LexSharp<T> : Tokenizer<T> where T : struct
   {
      public LexSharp(IEnumerable<Pattern<T>> patterns, IEnumerable<Pattern<T>> patternsToIgnor) : base(patterns, patternsToIgnor)
      {}


      public override IEnumerable<Token<T>> Scan(string text)
      {
         var TextCursor = new TextCursor<T>(text, Patterns);
         var tokens = Enumerable.Range(0, int.MaxValue)
                                .Select(x => TextCursor.GetNextToken())
                                .TakeWhile(r => TextCursor.IsNotFinished).ToList();
         var validTokens = tokens.Where( tok => (tok.Type == null) || (!IgnoreTokenType.Contains((T)tok.Type)));
         return validTokens;
      }
   }
}
