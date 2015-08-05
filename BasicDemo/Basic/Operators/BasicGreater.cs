using System;

namespace BasicDemo.Basic
{
   public class BasicGreater : BasicBinaryOperator
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
            return new BasicBoolean((left as BasicInteger).Value > (right as BasicInteger).Value);
         }
         if (left.IsReal && right.IsInteger)
         {
            return new BasicBoolean((left as BasicReal).Value - (double)(right as BasicInteger).Value > double.Epsilon);
         }
         if (left.IsInteger && right.IsReal)
         {
            return new BasicBoolean((double)(left as BasicInteger).Value - (right as BasicReal).Value > double.Epsilon);
         }
         if (left.IsReal && right.IsReal)
         {
            return new BasicBoolean((left as BasicReal).Value - (right as BasicReal).Value > double.Epsilon);
         }
         throw new BasicTypeMissmatchException("Can not compare wrong types.");
      }
   }
}
