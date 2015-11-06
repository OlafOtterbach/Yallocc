using System;

namespace LexSharp
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
