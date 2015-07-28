namespace BasicDemo.Basic
{
   public class BasicMultiplication : BasicBinaryOperator
   {
      public override BasicEntity Execute(BasicEntity left, BasicEntity right)
      {
         if(left.IsArray)
         {
            left = (left as BasicArrayElementAccessor).Value;
         }
         if (right.IsArray)
         {
            right = (right as BasicArrayElementAccessor).Value;
         }
         if (left.IsInteger && right.IsInteger)
         {
            return new BasicInteger((left as BasicInteger).Value * (right as BasicInteger).Value);
         }
         if (left.IsFloat && right.IsInteger)
         {
            return new BasicFloat((left as BasicFloat).Value * (double)(right as BasicInteger).Value);
         }
         if (left.IsInteger && right.IsFloat)
         {
            return new BasicFloat((double)(left as BasicInteger).Value * (right as BasicFloat).Value);
         }
         if (left.IsFloat && right.IsFloat)
         {
            return new BasicFloat((left as BasicFloat).Value * (right as BasicFloat).Value);
         }
         throw new BasicTypeMissmatchException("Can not multiply wrong types.");
      }
   }
}
