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

      public override BasicVariable Clone()
      {
         return new BasicInteger(Value);
      }

      public override BasicEntity CreateNew()
      {
         return new BasicInteger(0);
      }

      public override void Set(BasicInteger second)
      {
         Value = second.Value;
      }

      public override void Set(BasicReal second)
      {
         Value = (int)second.Value;
      }

      public override void Set(BasicString second)
      {
         string text = second.Value;
         int outVal;
         if(int.TryParse(text, out outVal))
         {
            Value = outVal;
         }
         else
         {
            Value = 0;
         }
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
