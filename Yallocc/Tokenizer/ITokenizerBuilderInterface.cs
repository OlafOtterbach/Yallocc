namespace LexSharp
{
   public interface ITokenizerBuilderInterface<T> where T : struct
   {
      ITokenizerBuilderInterface<T> Register(string patternText, T tokenType);
      ITokenizerBuilderInterface<T> RegisterIgnorePattern(string patternText, T tokenType);
      ITokenizer<T> Initialize();
   }
}
