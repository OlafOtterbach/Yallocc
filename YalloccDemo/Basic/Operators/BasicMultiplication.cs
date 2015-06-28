namespace YalloccDemo.Basic
{
   public class BasicMultiplication : BasicBinaryOperator
   {
      protected override int ExecuteIntegerInteger(int left, int right)
      {
         return left * right;
      }

      protected override double ExecuteFloatFloat(double left, double right)
      {
         return left * right;
      }

      protected override bool ExecuteBooleanBoolean(bool left, bool right)
      {
         throw new TypeMissmatchException("Can not multiply two boolean values.");
      }

      protected override bool ExecuteStringString(string left, string right)
      {
         throw new TypeMissmatchException("Can not multiply two string values.");
      }
   }
}
