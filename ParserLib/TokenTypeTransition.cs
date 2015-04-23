using System;

namespace ParserLib
{
   public class TokenTypeTransition<T> : Transition
   {
      public TokenTypeTransition( T tokenType ) : base()
      {
         TokenType = tokenType;
      }

      public TokenTypeTransition(string name, T tokenType) : base()
      {
         Name = name;
         TokenType = tokenType;
      }

      public TokenTypeTransition(T tokenType, Action action) : base(action)
      {
         TokenType = tokenType;
      }

      public TokenTypeTransition(string name, T tokenType, Action action) : base(action)
      {
         Name = name;
         TokenType = tokenType;
      }

      public T TokenType { get; private set; }
   }
}
