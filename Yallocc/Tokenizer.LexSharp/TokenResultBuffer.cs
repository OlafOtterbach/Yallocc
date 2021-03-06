﻿/// <summary>Tokenizer</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

namespace Yallocc.Tokenizer.LexSharp
{
   public class TokenResultBuffer<T> where T : struct
   {
      private Token<T> _buffer;

      public TokenResultBuffer()
      {
         _buffer = new Token<T>(string.Empty,0,0);
         IsEmpty = true;
      }

      public bool IsEmpty { get; private set; }

      public Token<T> Content
      {
         set
         {
            _buffer = value;
            IsEmpty = false;
         }
         get
         {
            IsEmpty = true;
            var result = _buffer;
            _buffer = new Token<T>(string.Empty, 0, 0);
            return result;
         }
      }
   }
}
