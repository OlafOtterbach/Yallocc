using System.Linq;

namespace LexSharp
{
   public enum TokenType
   {
      a_token,
      b_token,
      c_token,
      aXYZb_token,
      aabb_token
   }

   class Program
   {
      private static LexSharp<TokenType> CreateAbcLex()
      {
         var lex = new LexSharp<TokenType>();
         lex.Register(@"(a)+", TokenType.a_token);
         lex.Register(@"aabb", TokenType.aabb_token);
         lex.Register(@"a(\w)+b", TokenType.aXYZb_token);
         lex.Register(@"(b)+", TokenType.b_token);
         lex.Register(@"(c)+", TokenType.c_token);
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
