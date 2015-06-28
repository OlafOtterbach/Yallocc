using System;

namespace YalloccDemo.Basic
{
   public class TypeMissmatchException : Exception
   {
      public TypeMissmatchException()
      {
      }

      public TypeMissmatchException(string message)
         : base(message)
      {
      }

      public TypeMissmatchException(string message, Exception inner)
         : base(message, inner)
      {
      }
   }
}
