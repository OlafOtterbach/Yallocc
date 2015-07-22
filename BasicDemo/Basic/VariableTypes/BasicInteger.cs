namespace BasicDemo.Basic
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

      public abstract BasicEntity CreateNew()
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
