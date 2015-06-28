namespace YalloccDemo.Basic
{
   public class DefCommand : BasicCommand
   {
      public DefCommand(BasicEngine machine) : base(machine)
      {}

      public DefCommand(BasicEngine machine, ExpressionCommand expression)
         : base(machine)
      { }

      public string Name { get; set; }

      public BasicEntity Variable { get; set; }

      public override void Execute()
      {
         Machine.Add(Name, Variable);
      }
   }
}
