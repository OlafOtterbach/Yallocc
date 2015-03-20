using LexSharp;

namespace ParserLib
{
   public class SyntaxTokenNode<T> : SyntaxNode
   {
      public Token<T> Token { get; set; } 
   }
}
