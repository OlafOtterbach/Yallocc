namespace BasicDemo.Basic
{
   public abstract class BasicCommand
   {
      public BasicCommand(BasicEngine machine)
      {
         Machine = machine;
      }

      public BasicEngine Machine { get; private set; }

      public abstract void Execute();
   }
}
