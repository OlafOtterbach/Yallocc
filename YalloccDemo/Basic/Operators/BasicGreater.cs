using System;

namespace YalloccDemo.Basic
{
   public class BasicGreater : BasicBinaryOperator
   {
      public override BasicEntity Execute(BasicEntity left, BasicEntity right)
      {
         if (left.IsInteger && right.IsInteger)
         {
            return new BasicBoolean((left as BasicInteger).Value > (right as BasicInteger).Value);
         }
         if (left.IsFloat && right.IsInteger)
         {
            return new BasicBoolean((left as BasicFloat).Value - (double)(right as BasicInteger).Value > double.Epsilon);
         }
         if (left.IsInteger && right.IsFloat)
         {
            return new BasicBoolean((double)(left as BasicInteger).Value - (right as BasicFloat).Value > double.Epsilon);
         }
         if (left.IsFloat && right.IsFloat)
         {
            return new BasicBoolean((left as BasicFloat).Value - (right as BasicFloat).Value > double.Epsilon);
         }
         throw new TypeMissmatchException("Can not compare wrong types.");
      }
   }
}
