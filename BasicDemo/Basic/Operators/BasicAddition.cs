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
         if (left.IsFloat && right.IsInteger)
         {
            return new BasicFloat((left as BasicFloat).Value + (double)(right as BasicInteger).Value);
         }
         if (left.IsInteger && right.IsFloat)
         {
            return new BasicFloat((double)(left as BasicInteger).Value + (right as BasicFloat).Value);
         }
         if (left.IsFloat && right.IsFloat)
         {
            return new BasicFloat((left as BasicFloat).Value + (right as BasicFloat).Value);
         }
         if (left.IsString && right.IsString)
         {
            return new BasicString((left as BasicString).Value + (right as BasicString).Value);
         }
         if (left.IsString && right.IsInteger)
         {
            return new BasicString((left as BasicString).Value + (right as BasicInteger).Value.ToString());
         }
         if (left.IsString && right.IsFloat)
         {
            return new BasicString((left as BasicString).Value + (right as BasicFloat).Value.ToString());
         }
         if (left.IsString && right.IsBoolean)
         {
            return new BasicString((left as BasicString).Value + (right as BasicBoolean).Value.ToString());
         }
         if (left.IsInteger && right.IsString)
         {
            return new BasicString((left as BasicInteger).Value.ToString() + (right as BasicString).Value);
         }
         if (left.IsFloat && right.IsString)
         {
            return new BasicString((left as BasicFloat).Value.ToString() + (right as BasicString).Value);
         }
         if (left.IsBoolean && right.IsString)
         {
            return new BasicString((left as BasicBoolean).Value.ToString() + (right as BasicString).Value);
         }

         throw new BasicTypeMissmatchException("Can not add wrong types.");
      }
   }
}
