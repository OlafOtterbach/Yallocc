namespace YalloccDemo.Basic
{
   public struct BasicFloat : BasicType
   {
      public BasicFloat(string name, double value)
      {
         Value = value;
         Name = name;
         IsBoolean = false;
         IsInteger = false;
         IsFloat = true;
         IsString = false;
      }

      public double Value { get; set; }

      public string Name { get; private set; }
      public bool IsBoolean { get; private set; }
      public bool IsInteger { get; private set; }
      public bool IsFloat { get; private set; }
      public bool IsString { get; private set; }
   }
}
