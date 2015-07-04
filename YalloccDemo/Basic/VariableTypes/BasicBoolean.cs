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

      public override BasicType Type
      {
         get
         {
            return BasicType.e_boolean;
         }
      }

      public bool Value { get; set; }
   }
}
