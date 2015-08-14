using System;

namespace BasicDemo.Basic
{
   public class BasicVariableAlreadyDefinedException : BasicException
   {
      public BasicVariableAlreadyDefinedException()
      {
      }

      public BasicVariableAlreadyDefinedException(string message)
         : base(message)
      {
      }

      public BasicVariableAlreadyDefinedException(string message, Exception inner)
         : base(message, inner)
      {
      }
   }
}
