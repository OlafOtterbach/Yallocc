using System;

namespace YalloccDemo.Basic
{
   class BasicLess : BasicBinaryOperator
   {
      protected override bool ExecuteIntegerInteger(int left, int right)
      {
         return left < right;
      }

      protected override bool ExecuteFloatFloat(double left, double right)
      {
         return right - left > double.Epsilon;
      }

      protected override bool ExecuteBooleanBoolean(bool left, bool right)
      {
         throw new TypeMissmatchException("Can not compare two boolean values.");
      }

      protected override bool ExecuteStringString(string left, string right)
      {
         throw new TypeMissmatchException("Can not compare two string values.");
      }
   }
}
