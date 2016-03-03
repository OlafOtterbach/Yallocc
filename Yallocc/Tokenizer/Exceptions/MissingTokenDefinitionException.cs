/// <summary>Tokenizer</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

using System;

namespace Yallocc.Tokenizer
{
   public class MissingTokenDefinitionException : Exception
   {
      public MissingTokenDefinitionException()
      {
      }

      public MissingTokenDefinitionException(string message) : base(message)
      {
      }

      public MissingTokenDefinitionException(string label, string message) : base(message)
      {
      }

      public MissingTokenDefinitionException(string message, Exception inner) : base(message, inner)
      {
      }
   }
}
