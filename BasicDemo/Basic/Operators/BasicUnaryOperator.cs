using System;

namespace BasicDemo.Basic
{
   public abstract class BasicUnaryOperator : BasicEntity
   {
      public override BasicType Type
      {
         get
         {
            return BasicType.e_unary_operator;
         }
      }

      public abstract BasicEntity Execute(BasicEntity variable);
   }
}
