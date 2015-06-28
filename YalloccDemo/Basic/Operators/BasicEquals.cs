using System;
namespace YalloccDemo.Basic
{
   public class BasicEquals : BasicBinaryOperator
   {
      protected override bool ExecuteIntegerInteger(int left, int right)
      {
         return left == right;
      }

      protected override bool ExecuteFloatFloat(double left, double right)
      {
         return Math.Abs(left - right) <= double.Epsilon;
      }

      protected override bool ExecuteBooleanBoolean(bool left, bool right)
      {
         return left == right;
      }

      protected override bool ExecuteStringString(string left, string right)
      {
         return left == right;
      }
   }
}
