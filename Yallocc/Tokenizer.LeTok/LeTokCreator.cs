﻿/// <summary>LeTok</summary>
/// <summary>[Le]exical [Tok]enizer.</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

namespace Yallocc.Tokenizer.LeTok
{
   public class LeTokCreator<T> : TokenizerCreator<T> where T :struct
   {
      public override Tokenizer<T> Create()
      {
         return new LeTok<T>(Patterns, PatternsToIgnore);
      }
   }
}
