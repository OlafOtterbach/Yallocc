using System;

namespace BasicDemo.Basic
{
   public class BasicExpressionFailureException : BasicException
   {
      public BasicExpressionFailureException()
      {
      }

      public BasicExpressionFailureException(string message)
         : base(message)
      {
      }

      public BasicExpressionFailureException(string message, Exception inner)
         : base(message, inner)
      {
      }
   }
}
