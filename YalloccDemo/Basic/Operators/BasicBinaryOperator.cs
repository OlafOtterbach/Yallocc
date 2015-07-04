using System;

namespace YalloccDemo.Basic
{
   public abstract class BasicBinaryOperator : BasicEntity
   {
      public override BasicType Type
      {
         get
         {
            return BasicType.e_binary_operator;
         }
      }

      public abstract BasicEntity Execute(BasicEntity left, BasicEntity right);
   }
}
