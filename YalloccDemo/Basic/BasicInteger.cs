namespace YalloccDemo.Basic
{
   public struct BasicInteger : BasicType
   {
      public BasicInteger(string name, int value)
      {
         Value = value;
         Name = name;
         IsBoolean = false;
         IsInteger = true;
         IsFloat = false;
         IsString = false;
      }

      public int Value { get; set; }

      public string Name { get; private set; }
      public bool IsBoolean { get; private set; }
      public bool IsInteger { get; private set; }
      public bool IsFloat { get; private set; }
      public bool IsString { get; private set; }
   }
}
