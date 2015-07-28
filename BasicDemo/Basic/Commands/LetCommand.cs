using System.Collections.Generic;

namespace BasicDemo.Basic
{
   public class LetCommand : BasicCommand
   {
      public LetCommand(BasicEngine machine) : base(machine)
      {
         Expression = new ExpressionCommand(new List<BasicEntity>().ToArray());
      }

      public LetCommand(BasicEngine machine, string name, ExpressionCommand expression) : base(machine)
      {
         Name = name;
         Expression = expression;
      }

      public string Name { get; set; }

      public ExpressionCommand Expression { get; set; }

      public override void Execute()
      {
         var variable = Expression.Execute();
         Machine.RegisterVariable(Name, variable);
      }
   }
}
