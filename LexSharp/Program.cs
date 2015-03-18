using System;
using System.Linq;

namespace LexSharp
{
   public enum AbTokenTypes
   {
      a_token,
      b_token,
      c_token,
      aXYZb_token,
      aabb_token
   }

   public struct AbTokenType : ITokenType
   {
      public AbTokenType(AbTokenTypes type)
         : this()
      {
         TokenType = type;
      }

      public AbTokenTypes TokenType { get; private set; }

      public override bool Equals(Object obj)
      {
         return (obj is ITokenType) ? Equals((ITokenType)obj) : false;
      }

      public bool Equals(ITokenType other)
      {
         return (other is AbTokenType) ? Equals((AbTokenType)other) : false;
      }

      public bool Equals(AbTokenType other)
      {
         return (TokenType == other.TokenType);
      }

      public override int GetHashCode()
      {
         return (int)TokenType;
      }
   }

   class Program
   {
      private static  Lex CreateAbcLex()
      {
         var lex = new Lex();
         lex.Register(@"(a)+", new AbTokenType(AbTokenTypes.a_token));
         lex.Register(@"aabb", new AbTokenType(AbTokenTypes.aabb_token));
         lex.Register(@"a(\w)+b", new AbTokenType(AbTokenTypes.aXYZb_token));
         lex.Register(@"(b)+", new AbTokenType(AbTokenTypes.b_token));
         lex.Register(@"(c)+", new AbTokenType(AbTokenTypes.c_token));
         return lex;
      }

      static void Main(string[] args)
      {
         var lex = CreateAbcLex();
         var text = "aa\nbb\ncc\naabb\naccb\n";

         var tokens = lex.Scan(text).ToList();

      }
   }
}
