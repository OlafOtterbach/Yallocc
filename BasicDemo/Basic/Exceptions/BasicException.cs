using System;

namespace BasicDemo.Basic
{
   public class BasicException : Exception
   {
      public BasicException()
      {
      }

      public BasicException(string message)
         : base(message)
      {
      }

      public BasicException(string message, Exception inner)
         : base(message, inner)
      {
      }

      public int StartPosition { get; set; }
   }
}
