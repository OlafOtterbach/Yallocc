namespace BasicDemo.Basic
{
   public class BasicAddition : BasicBinaryOperator
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
            return new BasicInteger((left as BasicInteger).Value + (right as BasicInteger).Value);
         }
         if (left.IsReal && right.IsInteger)
         {
            return new BasicReal((left as BasicReal).Value + (double)(right as BasicInteger).Value);
         }
         if (left.IsInteger && right.IsReal)
         {
            return new BasicReal((double)(left as BasicInteger).Value + (right as BasicReal).Value);
         }
         if (left.IsReal && right.IsReal)
         {
            return new BasicReal((left as BasicReal).Value + (right as BasicReal).Value);
         }
         if (left.IsString && right.IsString)
         {
            return new BasicString((left as BasicString).Value + (right as BasicString).Value);
         }
         if (left.IsString && right.IsInteger)
         {
            return new BasicString((left as BasicString).Value + (right as BasicInteger).Value.ToString());
         }
         if (left.IsString && right.IsReal)
         {
            return new BasicString((left as BasicString).Value + (right as BasicReal).Value.ToString());
         }
         if (left.IsString && right.IsBoolean)
         {
            return new BasicString((left as BasicString).Value + (right as BasicBoolean).Value.ToString());
         }
         if (left.IsInteger && right.IsString)
         {
            return new BasicString((left as BasicInteger).Value.ToString() + (right as BasicString).Value);
         }
         if (left.IsReal && right.IsString)
         {
            return new BasicString((left as BasicReal).Value.ToString() + (right as BasicString).Value);
         }
         if (left.IsBoolean && right.IsString)
         {
            return new BasicString((left as BasicBoolean).Value.ToString() + (right as BasicString).Value);
         }

         throw new BasicTypeMissmatchException("Can not add wrong types.");
      }
   }
}
