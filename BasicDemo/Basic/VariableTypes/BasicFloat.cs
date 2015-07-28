namespace BasicDemo.Basic
{
   public class BasicFloat : BasicVariable
   {
      public BasicFloat()
      {
      }

      public BasicFloat(double value)
      {
         Value = value;
      }

      public override BasicEntity CreateNew()
      {
         return new BasicFloat(0.0);
      }

      public override BasicType Type
      {
         get
         {
            return BasicType.e_float;
         }
      }

      public double Value { get; set; }
   }
}
