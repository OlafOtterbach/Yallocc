namespace LexSharp
{
   struct TokenResult<T>
   {
      public Token<T> Token {get; set;}
      public bool IsValid { get; set; }
   }
}
