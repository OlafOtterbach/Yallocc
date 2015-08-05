namespace BasicDemo.Basic
{
   public class BasicReal : BasicVariable
   {
      public BasicReal()
      {
      }

      public BasicReal(double value)
      {
         Value = value;
      }

      public override BasicEntity CreateNew()
      {
         return new BasicReal(0.0);
      }

      public override BasicType Type
      {
         get
         {
            return BasicType.e_real;
         }
      }

      public double Value { get; set; }
   }
}
