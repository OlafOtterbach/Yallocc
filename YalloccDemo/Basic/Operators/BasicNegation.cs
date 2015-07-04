namespace YalloccDemo.Basic
{
   public class BasicNegation : BasicUnaryOperator
   {
      public override BasicEntity Execute(BasicEntity variable)
      {
         if (variable.IsInteger)
         {
            return new BasicInteger(-(variable as BasicInteger).Value);
         }
         if (variable.IsFloat)
         {
            return new BasicFloat(-(variable as BasicFloat).Value);
         }
         throw new TypeMissmatchException("Can not negate wrong types.");
      }
   }
}
