namespace YalloccDemo.Basic
{
   public class BasicInteger : BasicEntity
   {
      public BasicInteger()
      {
      }

      public BasicInteger(int value)
      {
         Value = value;
      }

      protected override BasicType Type 
      { 
         get
         {
            return BasicType.e_integer;
         }
      }

      public int Value { get; set; }
   }
}
