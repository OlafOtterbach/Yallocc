namespace BasicDemo.Basic
{
   public class BasicInteger : BasicVariable
   {
      public BasicInteger()
      {
      }

      public BasicInteger(int value)
      {
         Value = value;
      }

      public override BasicEntity CreateNew()
      {
         return new BasicInteger(0);
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
