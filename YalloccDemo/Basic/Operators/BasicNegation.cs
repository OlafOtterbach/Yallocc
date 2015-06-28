namespace YalloccDemo.Basic
{
   public class BasicNegationSign : BasicUnaryOperator
   {
      protected override int ExecuteInteger(int variable)
      {
         return -variable;
      }

      protected override double ExecuteFloat(double variable)
      {
         return -variable;
      }
   }
}
