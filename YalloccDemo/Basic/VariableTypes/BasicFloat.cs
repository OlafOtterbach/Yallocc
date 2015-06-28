namespace YalloccDemo.Basic
{
   public class BasicFloat : BasicEntity
   {
      public BasicFloat()
      {
      }

      public BasicFloat(double value)
      {
         Value = value;
      }

      protected override BasicType Type
      {
         get
         {
            return BasicType.e_float;
         }
      }

      public double Value { get; set; }
   }
}
