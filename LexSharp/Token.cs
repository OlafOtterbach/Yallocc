namespace LexSharp
{
   public struct Token<T>
   {
      public Token(string value, T type, int textIndex, int length) : this()
      {
         Value = value;
         Type = type;
         TextIndex = textIndex;
         Length = length;
      }

      public string Value { get; private set; }
      public T Type { get; private set; }
      public int TextIndex { get; private set; }
      public int Length { get; private set; }
   }
}
