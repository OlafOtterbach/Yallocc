namespace BasicDemo.Basic
{
   public class BasicAdditionSign : BasicUnaryOperator
   {
      public override BasicEntity Execute(BasicEntity variable)
      {
         if (variable.IsInteger)
         {
            return variable;
         }
         if (variable.IsFloat)
         {
            return variable;
         }
         throw new BasicTypeMissmatchException("Can not set plus before type.");
      }
   }
}
