using System;
namespace BasicDemo.Basic
{
   public class BasicEquals : BasicBinaryOperator
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
            return new BasicBoolean((left as BasicInteger).Value == (right as BasicInteger).Value);
         }
         if (left.IsFloat && right.IsInteger)
         {
            return new BasicBoolean(Math.Abs((left as BasicFloat).Value - (double)(right as BasicInteger).Value) <= double.Epsilon);
         }
         if (left.IsInteger && right.IsFloat)
         {
            return new BasicBoolean(Math.Abs((double)(left as BasicInteger).Value - (right as BasicFloat).Value) <= double.Epsilon);
         }
         if (left.IsFloat && right.IsFloat)
         {
            return new BasicBoolean(Math.Abs((left as BasicFloat).Value - (right as BasicFloat).Value) <= double.Epsilon);
         }
         if (left.IsString && right.IsString)
         {
            return new BasicBoolean((left as BasicString).Value == (right as BasicString).Value);
         }
         if (left.IsBoolean && right.IsBoolean)
         {
            return new BasicBoolean((left as BasicBoolean).Value == (right as BasicBoolean).Value);
         }
         throw new BasicTypeMissmatchException("Can not compare wrong types.");
      }
   }
}
