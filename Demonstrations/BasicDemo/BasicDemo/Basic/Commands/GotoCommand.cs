using System.Linq;
using Yallocc.Tokenizer;

namespace BasicDemo.Basic
{
   public class GotoCommand : BasicCommand
   {
      private string _name;

      public GotoCommand(Token<TokenType> startToken, BasicEngine engine, string name) : base(startToken, engine)
      {
         _name = name;
      }

      public override void Execute()
      {
         var labels = Engine.Program.OfType<LabelCommand>().Where(labelCommand => labelCommand.Name == _name).Take(1);
         if (labels.Any())
         {
            var label = labels.First();
            Engine.Cursor.GotoNextOfCommand(label);
         }
      }
   }
}
