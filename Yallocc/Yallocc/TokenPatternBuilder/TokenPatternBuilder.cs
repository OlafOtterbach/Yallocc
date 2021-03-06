﻿/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

using Yallocc.Tokenizer;

namespace Yallocc
{
   public class TokenPatternBuilder<T> where T : struct
   {
      private readonly TokenizerCreator<T> _tokenizerCreator;

      public TokenPatternBuilder(TokenizerCreator<T> tokenizerCreator)
      {
         _tokenizerCreator = tokenizerCreator;
      }

      public TokenPatternBuilder<T> AddTokenPattern(string patternText, T tokenType)
      {
         _tokenizerCreator.Register(patternText, tokenType);
         return this;
      }
      
      public TokenPatternBuilder<T> AddIgnorePattern(string patternText, T tokenType)
      {
         _tokenizerCreator.RegisterIgnorePattern(patternText, tokenType);
         return this;
      }

      public void End()
      {
         // fluent interface end.
      }
   }
}
