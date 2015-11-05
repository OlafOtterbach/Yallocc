namespace BasicDemo.Basic
{
   public class BasicModulo : BasicBinaryOperator
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
         if (left.IsInteger && right.IsInteger)
         {
            return new BasicInteger((left as BasicInteger).Value % (right as BasicInteger).Value);
         }
         throw new BasicTypeMissmatchException("Can not multiply wrong types.");
      }
   }
}