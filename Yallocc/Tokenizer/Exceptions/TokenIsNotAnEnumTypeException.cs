/// <summary>Tokenizer</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

using System;

namespace Yallocc.Tokenizer
{
   public class TokenIsNotAnEnumTypeException : Exception
   {
      public TokenIsNotAnEnumTypeException()
      {
      }

      public TokenIsNotAnEnumTypeException(string message)
         : base(message)
      {
      }

      public TokenIsNotAnEnumTypeException(string message, Exception inner)
         : base(message, inner)
      {
      }
   }
}
