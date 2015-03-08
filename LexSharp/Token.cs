namespace LexSharp
{
   public struct Token
   {
      public Token(string value, ITokenType type, int index, int length) : this()
      {
         Value = value;
         Type = type;
         Index = index;
         Length = length;
      }

      public string Value { get; private set; }
      public ITokenType Type { get; private set; }
      public int Index { get; private set; }
      public int Length { get; private set; }
   }
}
