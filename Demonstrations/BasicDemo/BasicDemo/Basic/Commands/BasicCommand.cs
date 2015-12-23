using Yallocc.Tokenizer;

namespace BasicDemo.Basic
{
   public abstract class BasicCommand
   {
      public BasicCommand(Token<TokenType> startToken, BasicEngine engine)
      {
         Engine = engine;
         StartToken = startToken;
      }

      public BasicEngine Engine { get; }

      protected Token<TokenType> StartToken { get; }

      public abstract void Execute();
   }
}
