namespace BasicDemo.Basic
{
   public class BasicNot : BasicUnaryOperator
   {
      public override BasicEntity Execute(BasicEntity variable)
      {
         if (variable.IsArray)
         {
            variable = (variable as BasicArrayElementAccessor).Value;
         }
         if (variable.IsBoolean)
         {
            return new BasicBoolean(!(variable as BasicBoolean).Value);
         }
         throw new BasicTypeMissmatchException("Can not set plus before type.");
      }
   }
}