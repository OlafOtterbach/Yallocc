namespace BasicDemo.Basic
{
   public class BasicNegation : BasicUnaryOperator
   {
      public override BasicEntity Execute(BasicEntity variable)
      {
         if (variable.IsArray)
         {
            variable = (variable as BasicArrayElementAccessor).Value;
         }
         if (variable.IsInteger)
         {
            return new BasicInteger(-(variable as BasicInteger).Value);
         }
         if (variable.IsReal)
         {
            return new BasicReal(-(variable as BasicReal).Value);
         }
         throw new BasicTypeMissmatchException("Can not negate wrong types.");
      }
   }
}
