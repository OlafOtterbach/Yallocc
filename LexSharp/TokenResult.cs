namespace LexSharp
{
   public struct TokenResult<T>
   {
      public Token<T> Token {get; set;}
      public bool IsValid { get; set; }
   }
}
