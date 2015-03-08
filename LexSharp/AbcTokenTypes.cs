using System;

namespace LexSharp
{
   public struct AbcTokenType : ITokenType 
   {
      public enum AbcTokenTypes
      {
         a_token,
         b_token,
         c_token
      }

      public AbcTokenType(AbcTokenTypes type) : this()
      {
         TokenType = type;
      }

      public AbcTokenTypes TokenType { get; private set; }

      public override bool Equals(Object obj)
      {
         return (obj is ITokenType) ? Equals( (ITokenType)obj ) : false;
      }

      public bool Equals(ITokenType other)
      {
         return (other is AbcTokenType) ? Equals((AbcTokenType)other) : false;
      }

      public bool Equals(AbcTokenType other)
      {
         return (TokenType == other.TokenType);
      }

      public override int GetHashCode()
      {
         return (int)TokenType;
      }
   }
}
