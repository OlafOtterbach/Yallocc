using System;

namespace YalloccDemo.Basic
{
   public abstract class BasicBinaryOperator : BasicEntity
   {
      protected override BasicType Type
      {
         get
         {
            return BasicType.e_binary_operator;
         }
      }

      protected abstract int ExecuteIntegerInteger(int left, int right);
      protected abstract double ExecuteFloatFloat(double left, double right);
      protected abstract bool ExecuteBooleanBoolean(bool left, bool right);
      protected abstract bool ExecuteStringString(string left, string right);

      public BasicEntity Execute(BasicEntity left, BasicEntity right)
      {
         if(left.IsInteger && right.IsInteger)
         {
            return new BasicInteger() { Value = ExecuteIntegerInteger((left as BasicInteger).Value, (left as BasicInteger).Value) };
         }
         if (left.IsInteger && right.IsFloat)
         {
            return new BasicFloat() { Value = ExecuteFloatFloat((double)(left as BasicInteger).Value, (left as BasicFloat).Value) };
         }
         if (left.IsFloat && right.IsInteger)
         {
            return new BasicFloat() { Value = ExecuteFloatFloat((left as BasicFloat).Value, (double)(left as BasicInteger).Value) };
         }
         if (left.IsFloat && right.IsFloat)
         {
            return new BasicFloat() { Value = ExecuteFloatFloat((left as BasicFloat).Value, (left as BasicFloat).Value) };
         }
         if (left.IsBoolean && right.IsBoolean)
         {
            return new BasicBoolean() { Value = ExecuteBooleanBoolean((left as BasicBoolean).Value, (left as BasicBoolean).Value) };
         }
         if (left.IsString && right.IsString)
         {
            return new BasicBoolean() { Value = ExecuteStringString((left as BasicString).Value, (left as BasicString).Value) };
         }
         return null;
      }
   }
}
