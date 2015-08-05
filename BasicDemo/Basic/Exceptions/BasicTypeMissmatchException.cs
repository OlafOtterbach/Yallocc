using System;

namespace BasicDemo.Basic
{
   public class BasicTypeMissmatchException : BasicException
   {
      public BasicTypeMissmatchException()
      {
      }

      public BasicTypeMissmatchException(string message)
         : base(message)
      {
      }

      public BasicTypeMissmatchException(string message, Exception inner)
         : base(message, inner)
      {
      }
   }
}
