using LexSharp;

namespace BasicDemo.Basic
{
   public class EndOfProgramCommand : BasicCommand
   {
      public EndOfProgramCommand(Token<TokenType> startToken, BasicEngine engine) : base(startToken, engine)
      {}

      public override void Execute()
      {}
   }
}
