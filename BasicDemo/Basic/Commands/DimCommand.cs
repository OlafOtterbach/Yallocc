using LexSharp;
using System.Linq;

namespace BasicDemo.Basic
{
   public class DimCommand : BasicCommand
   {
      public DimCommand(Token<TokenType> startToken, BasicEngine engine, string name, params ExpressionCommand[] expressions) : base(startToken, engine)
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
         try
         {
            Engine.RegisterVariable(Name, array);
         }
         catch(BasicVariableAlreadyDefinedException e)
         {
            e.StartPosition = StartToken.TextIndex;
            throw;
         }
         Engine.Cursor.Next();
      }
   }
}
