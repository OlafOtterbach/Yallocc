using System;

namespace YalloccDemo.Basic
{
   public abstract class BasicUnaryOperator : BasicEntity
   {
      protected override BasicType Type
      {
         get
         {
            return BasicType.e_unary_operator;
         }
      }

      protected abstract int ExecuteInteger(int variable);
      protected abstract double ExecuteFloat(double variable);

      public BasicEntity Execute(BasicEntity variable)
      {
         if (variable.IsInteger)
         {
            return new BasicInteger() { Value = ExecuteInteger((variable as BasicInteger).Value) };
         }
         if (variable.IsFloat)
         {
            return new BasicFloat() { Value = ExecuteFloat((double)(variable as BasicInteger).Value) };
         }
         return null;
      }
   }
}
