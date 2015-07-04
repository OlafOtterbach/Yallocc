namespace YalloccDemo.Basic
{
   public class BasicAddition : BasicBinaryOperator
   {
      public override BasicEntity Execute(BasicEntity left, BasicEntity right)
      {
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
         throw new TypeMissmatchException("Can not add wrong types.");
      }
   }
}
