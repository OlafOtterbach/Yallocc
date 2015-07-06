using System;

namespace BasicDemo.Basic
{
   public class BasicOutOfRangeException : Exception
   {
      public BasicOutOfRangeException()
      {
      }

      public BasicOutOfRangeException(string message)
         : base(message)
      {
      }

      public BasicOutOfRangeException(string message, Exception inner)
         : base(message, inner)
      {
      }
   }
}
