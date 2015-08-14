using System.Globalization;
namespace BasicDemo.Basic
{
   public class BasicReal : BasicVariable
   {
      public BasicReal()
      {
      }

      public BasicReal(double value)
      {
         Value = value;
      }

      public override void Set(BasicInteger second)
      {
         Value = (double)second.Value;
      }

      public override void Set(BasicReal second)
      {
         Value = second.Value;
      }

      public override void Set(BasicString second)
      {
         string text = second.Value;
         double outVal;
         if (double.TryParse(text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out outVal))
         {
            Value = outVal;
         }
         else
         {
            Value = 0;
         }
      }

      public override BasicEntity CreateNew()
      {
         return new BasicReal(0.0);
      }

      public override BasicType Type
      {
         get
         {
            return BasicType.e_real;
         }
      }

      public double Value { get; set; }
   }
}
