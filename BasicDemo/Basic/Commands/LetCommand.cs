using LexSharp;
using System.Collections.Generic;
using System.Linq;

namespace BasicDemo.Basic
{
   public class LetCommand : BasicCommand
   {
      public string _name;

      public ExpressionCommand _expression;

      public ExpressionCommand[] _arrayExpressions;

      public LetCommand(Token<TokenType> startToken, BasicEngine engine, string name, ExpressionCommand expression) : base(startToken, engine)
      {
         _name = name;
         _expression = expression;
         _arrayExpressions = new ExpressionCommand[0];
      }

      public LetCommand(Token<TokenType> startToken, BasicEngine engine, string name, IEnumerable<ExpressionCommand> arrayExpressions, ExpressionCommand expression) : base(startToken, engine)
      {
         _name = name;
         _expression = expression;
         _arrayExpressions = arrayExpressions.ToArray();
      }

      public override void Execute()
      {
         var variable = _expression.Execute();
         if (Engine.HasVariable(_name))
         {
            try
            {
               var entity = Engine.GetVariable(_name);
               if (entity is BasicVariable)
               {
                  (entity as BasicVariable).Set(variable);
               }
               else
               {
                  if( entity is BasicArray)
                  {
                     var accessor = new BasicArrayElementAccessor((entity as BasicArray), _arrayExpressions);
                     if (accessor.Value != null)
                     {
                        (accessor.Value as BasicVariable).Set(variable);
                     }
                     else
                     {
                        accessor.Set(variable);
                     }
                  }
               }
            }
            catch (BasicTypeMissmatchException typeMissmatchException)
            {
               typeMissmatchException.StartPosition = StartToken.TextIndex;
               throw;
            }
         }
         else
         {
            var newVariable = (variable as BasicVariable).Clone();
            Engine.RegisterVariable(_name, newVariable);
         }
         Engine.Cursor.Next();
      }
   }
}
