namespace YalloccDemo.Basic
{
   public class BasicBoolean : BasicEntity
   {
      public BasicBoolean()
      {
      }

      public BasicBoolean(bool value)
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

      public bool Value { get; set; }
   }
}
