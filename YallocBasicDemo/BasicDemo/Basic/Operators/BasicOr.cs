namespace BasicDemo.Basic
{
   public class BasicOr : BasicBinaryOperator
   {
      public override BasicEntity Execute(BasicEntity left, BasicEntity right)
      {
         if (left.IsArray)
         {
            left = (left as BasicArrayElementAccessor).Value;
         }
         if (right.IsArray)
         {
            right = (right as BasicArrayElementAccessor).Value;
         }
         if (left.IsBoolean && right.IsBoolean)
         {
            return new BasicBoolean((left as BasicBoolean).Value || (right as BasicBoolean).Value);
         }
         throw new BasicTypeMissmatchException("Can not multiply wrong types.");
      }
   }
}