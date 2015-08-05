namespace BasicDemo.Basic
{
   public class BasicDivision : BasicBinaryOperator
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
            return new BasicInteger((left as BasicInteger).Value / (right as BasicInteger).Value);
         }
         if (left.IsReal && right.IsInteger)
         {
            return new BasicReal((left as BasicReal).Value / (double)(right as BasicInteger).Value);
         }
         if (left.IsInteger && right.IsReal)
         {
            return new BasicReal((double)(left as BasicInteger).Value / (right as BasicReal).Value);
         }
         if (left.IsReal && right.IsReal)
         {
            return new BasicReal((left as BasicReal).Value / (right as BasicReal).Value);
         }
         throw new BasicTypeMissmatchException("Can not dividide wrong types.");
      }
   }
}
