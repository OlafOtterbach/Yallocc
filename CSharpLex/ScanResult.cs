namespace LexSharp
{
   struct ScanResult<T>
   {
      public Token<T> Token {get; set;}
      public bool IsValid { get; set; }
   }
}
