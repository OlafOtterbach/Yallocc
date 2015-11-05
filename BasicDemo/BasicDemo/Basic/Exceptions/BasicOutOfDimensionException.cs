using System;

namespace BasicDemo.Basic
{
   public class BasicOutOfDimensionExceptionusing : Exception
   {
      public BasicOutOfDimensionExceptionusing()
      {
      }

      public BasicOutOfDimensionExceptionusing(string message)
         : base(message)
      {
      }

      public BasicOutOfDimensionExceptionusing(string message, Exception inner)
         : base(message, inner)
      {
      }
   }
}
