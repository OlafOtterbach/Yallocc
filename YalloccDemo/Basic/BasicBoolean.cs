namespace YalloccDemo.Basic
{
   public struct BasicBoolean : BasicType
   {
      public BasicBoolean(string name, bool value)
      {
         Value = value;
         Name = name;
         IsBoolean = true;
         IsInteger = false;
         IsFloat = false;
         IsString = false;
      }

      public bool Value { get; set; }

      public string Name { get; private set; }
      public bool IsBoolean { get; private set; }
      public bool IsInteger { get; private set; }
      public bool IsFloat { get; private set; }
      public bool IsString { get; private set; }
   }
}
