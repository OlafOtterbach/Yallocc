using System;

namespace ParserLib
{
   public class MissingGotoLabelException : Exception
   {
      public MissingGotoLabelException()
      {
      }

      public MissingGotoLabelException(string message) : base(message)
      {
      }

      public MissingGotoLabelException(string label, string message) : base(message)
      {
         Label = label;
      }

      public MissingGotoLabelException(string message, Exception inner) : base(message, inner)
      {
      }

      public string Label { get; set; }
   }
}
