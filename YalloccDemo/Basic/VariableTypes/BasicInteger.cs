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

      public override BasicType Type 
      { 
         get
         {
            return BasicType.e_integer;
         }
      }

      public int Value { get; set; }
   }
}
