namespace LexSharp
{
   public class LeTokBuilder<T> : ITokenizerBuilderInterface<T> where T : struct
   {
      private ITokenizer<T> _tokenizer;

      private LeTokBuilder()
      {
         _tokenizer = new LeTok<T>();
      }

      public static ITokenizerBuilderInterface<T> Create()
      {
         var builder = new LeTokBuilder<T>();
         return builder;
      }

      public ITokenizer<T> Initialize()
      {
         _tokenizer.Initialize();
         return _tokenizer;
      }

      public ITokenizerBuilderInterface<T> Register(string patternText, T tokenType)
      {
         _tokenizer.Register(patternText, tokenType);
         return this;
      }

      public ITokenizerBuilderInterface<T> RegisterIgnorePattern(string patternText, T tokenType)
      {
         _tokenizer.RegisterIgnorePattern(patternText, tokenType);
         return this;
      }
   }
}
