namespace YalloccDemo.Basic
{
   public class BasicAdditionSign : BasicUnaryOperator
   {
      protected override int ExecuteInteger(int variable)
      {
         return variable;
      }

      protected override double ExecuteFloat(double variable)
      {
         return variable;
      }
   }
}
