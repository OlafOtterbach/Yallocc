namespace BasicDemo.Basic
{
   public class BasicAdditionSign : BasicUnaryOperator
   {
      public override BasicEntity Execute(BasicEntity variable)
      {
         if (variable.IsArray)
         {
            variable = (variable as BasicArrayElementAccessor).Value;
         }
         if (variable.IsInteger)
         {
            return variable;
         }
         if (variable.IsReal)
         {
            return variable;
         }
         throw new BasicTypeMissmatchException("Can not set plus before type.");
      }
   }
}
