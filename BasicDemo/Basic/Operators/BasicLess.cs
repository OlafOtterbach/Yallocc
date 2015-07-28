using System;

namespace BasicDemo.Basic
{
   public class BasicLess : BasicBinaryOperator
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
            return new BasicBoolean((left as BasicInteger).Value < (right as BasicInteger).Value);
         }
         if (left.IsFloat && right.IsInteger)
         {
            return new BasicBoolean((left as BasicFloat).Value - (double)(right as BasicInteger).Value < -double.Epsilon);
         }
         if (left.IsInteger && right.IsFloat)
         {
            return new BasicBoolean((double)(left as BasicInteger).Value - (right as BasicFloat).Value < -double.Epsilon);
         }
         if (left.IsFloat && right.IsFloat)
         {
            return new BasicBoolean((left as BasicFloat).Value - (right as BasicFloat).Value < -double.Epsilon);
         }
         throw new BasicTypeMissmatchException("Can not compare wrong types.");
      }
   }
}
