namespace ParserLib
{
   internal class TokenTypeTransition<T> : Transition
   {
      public TokenTypeTransition( T tokenType )
      {
         TokenType = tokenType;
      }

      public T TokenType { get; private set; }
   }
}
