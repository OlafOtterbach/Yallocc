namespace BasicDemo.Basic
{
   public class BasicBoolean : BasicVariable
   {
      public BasicBoolean()
      {
      }

      public BasicBoolean(bool value)
      {
         Value = value;
      }

      public override BasicVariable Clone()
      {
         return new BasicBoolean(Value);
      }

      public override void Set(BasicBoolean second)
      {
         Value = second.Value;
      }

      public override BasicEntity CreateNew()
      {
         return new BasicBoolean(false);
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
