namespace LexSharp
{
   class Program
   {
      static void Main(string[] args)
      {
         var lex = new Lex();
         lex.Register(new Pattern(@"(a)+", new AbcTokenType(AbcTokenType.AbcTokenTypes.a_token)));
         lex.Register(new Pattern(@"(b)+", new AbcTokenType(AbcTokenType.AbcTokenTypes.b_token)));
         lex.Register(new Pattern(@"(c)+", new AbcTokenType(AbcTokenType.AbcTokenTypes.c_token)));

         var text = @"aabbcc";
         lex.Scan(text);
      }
   }
}
