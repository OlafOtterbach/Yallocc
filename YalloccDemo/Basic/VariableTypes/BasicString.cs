namespace YalloccDemo.Basic
{
   public class BasicString : BasicEntity
   {
      public BasicString()
      {
      }

      public BasicString(string value)
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

      public string Value { get; set; }
   }
}
