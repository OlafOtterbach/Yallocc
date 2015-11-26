namespace LexSharp
{
   public class TokenizerBuilder<T> : ITokenizerBuilderInterface<T> where T : struct
   {
      private LeTok<T> _lex;

      private TokenizerBuilder()
      {
         _lex = new LeTok<T>();
      }

      public static ITokenizerBuilderInterface<T> Create()
      {
         var builder = new TokenizerBuilder<T>();
         return builder;
      }

      public LeTok<T> Init()
      {
         _lex.Init();
         return _lex;
      }

      public ITokenizerBuilderInterface<T> Register(string patternText, T tokenType)
      {
         _lex.Register(patternText, tokenType);
         return this;
      }

      public ITokenizerBuilderInterface<T> RegisterIgnorePattern(string patternText, T tokenType)
      {
         _lex.RegisterIgnorePattern(patternText, tokenType);
         return this;
      }
   }
}
