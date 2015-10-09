using System.Linq;
using LexSharp;

namespace BasicDemo.Basic
{
   public class WhileCommand : BasicCommand
   {
      private string _name;

      public ExpressionCommand _expression;

      public WhileCommand(Token<TokenType> startToken, BasicEngine engine, string name, ExpressionCommand expression) : base(startToken, engine)
      {
         _name = name;
         _expression = expression;
      }

      public override void Execute()
      {
         var variable = _expression.Execute();
         if (!(variable is BasicBoolean))
         {
            throw new BasicTypeMissmatchException("if expression is not of boolean type.");
         }
         var boolVariable = variable as BasicBoolean;
         var isNotTrue = !boolVariable.Value;
         if (isNotTrue)
         {
            var labels = Engine.Program.OfType<LabelCommand>().Where(labelCommand => labelCommand.Name == _name).Take(1);
            if (labels.Any())
            {
               var label = labels.First();
               Engine.Cursor.GotoNextOfCommand(label);
            }
         }
         else
         {
            Engine.Cursor.Next();
         }
      }
   }
}
