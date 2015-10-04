using LexSharp;

namespace BasicDemo.Basic
{
   class LabelCommand : BasicCommand
   {
      private static int _top = 1;
      private int _id;

      public LabelCommand(Token<TokenType> startToken, BasicEngine engine, string name) : base(startToken, engine)
      {
         _id = _top++;
         Name = name;
      }

      public LabelCommand(Token<TokenType> startToken, BasicEngine engine) : base(startToken, engine)
      {
         _id = _top++;
         Name = _id.ToString();
      }

      public string Name { get; set; }

      public override void Execute()
      {
         Engine.Cursor.Next();
      }
   }
}
