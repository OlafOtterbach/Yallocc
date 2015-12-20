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

      public BasicEngine Engine { get; private set; }

      protected Token<TokenType> StartToken { get; private set; }

      public abstract void Execute();
   }
}
