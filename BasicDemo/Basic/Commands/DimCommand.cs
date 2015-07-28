using System.Collections.Generic;
using System.Linq;

namespace BasicDemo.Basic
{
   public class DimCommand : BasicCommand
   {
      public DimCommand(BasicEngine machine) : base(machine)
      {
         Expressions = new List<ExpressionCommand>().ToArray();
      }

      public DimCommand(BasicEngine machine, string name, params ExpressionCommand[] expressions) : base(machine)
      {
         Name = name;
         Expressions = expressions;
      }

      public string Name { get; set; }

      public ExpressionCommand[] Expressions { get; set; }

      public override void Execute()
      {
         var indices = Expressions.Select(cmd => cmd.Execute())
                                    .OfType<BasicInteger>()
                                    .Select(intVar => intVar.Value)
                                    .ToArray();
         var array = new BasicArray(indices);
         Machine.RegisterVariable(Name, array);
      }
   }
}
