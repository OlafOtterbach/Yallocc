namespace YalloccDemo.Basic
{
   public struct BasicString : BasicType
   {
      public BasicString(string name, string value)
      {
         Value = value;
         Name = name;
         IsBoolean = false;
         IsInteger = false;
         IsFloat = false;
         IsString = true;
      }

      public string Value { get; set; }

      public string Name { get; private set; }
      public bool IsBoolean { get; private set; }
      public bool IsInteger { get; private set; }
      public bool IsFloat { get; private set; }
      public bool IsString { get; private set; }
   }
}
