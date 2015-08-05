using LexSharp;
using System.Collections.Generic;

namespace BasicDemo.Basic
{
   public class LetCommand : BasicCommand
   {
      public LetCommand(Token<TokenType> startToken, BasicEngine engine, string name, ExpressionCommand expression)
         : base(startToken, engine)
      {
         Name = name;
         Expression = expression;
      }

      public string Name { get; set; }

      public ExpressionCommand Expression { get; set; }

      public override void Execute()
      {
         var variable = Expression.Execute();
         Engine.RegisterVariable(Name, variable);
      }
   }
}
