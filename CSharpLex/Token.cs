namespace CSharpLex
{
   public struct Token
   {
      public Token(string value, ITokenType type, int textIndex, int length) : this()
      {
         Value = value;
         Type = type;
         TextIndex = textIndex;
         Length = length;
      }

      public string Value { get; private set; }
      public ITokenType Type { get; private set; }
      public int TextIndex { get; private set; }
      public int Length { get; private set; }
   }
}
