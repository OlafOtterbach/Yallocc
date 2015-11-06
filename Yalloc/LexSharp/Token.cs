using System;

namespace LexSharp
{
   public struct Token<T> where T : struct
   {
      public Token(T type) : this()
      {
         Value = string.Empty;
         Type = type;
         TextIndex = 0;
         Length = 0;
      }

      public Token(string value, int textIndex, int length) : this()
      {
         Value = value;
         Type = null;
         TextIndex = textIndex;
         Length = length;
      }

      public Token(string value, T type, int textIndex, int length) : this()
      {
         Value = value;
         Type = type;
         TextIndex = textIndex;
         Length = length;
      }

      public string Value { get; private set; }

      public Nullable<T> Type { get; private set; }

      public int TextIndex { get; private set; }

      public int Length { get; private set; }

      public bool IsValid
      {
         get
         {
            return Type != null;
         }
      }
   }
}
