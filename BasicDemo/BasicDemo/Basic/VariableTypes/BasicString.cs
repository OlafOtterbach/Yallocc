using System.Globalization;

namespace BasicDemo.Basic
{
   public class BasicString : BasicVariable
   {
      public BasicString()
      {
      }

      public BasicString(string value)
      {
         Value = value;
      }

      public override BasicVariable Clone()
      {
         return new BasicString(Value);
      }

      public override void Set(BasicInteger second)
      {
         Value = second.Value.ToString();
      }

      public override void Set(BasicReal second)
      {
         Value = second.Value.ToString(CultureInfo.InvariantCulture);
      }

      public override void Set(BasicString second)
      {
         Value = second.Value;
      }

      public override BasicEntity CreateNew()
      {
         return new BasicString(string.Empty);
      }

      public override BasicType Type
      {
         get
         {
            return BasicType.e_string;
         }
      }

      public string Value { get; set; }
   }
}
