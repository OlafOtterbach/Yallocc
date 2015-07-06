namespace BasicDemo.Basic
{
   public class BasicComandFactory
   {
      private BasicEngine _machine;

      public BasicComandFactory()
      {
         _machine = new BasicEngine();
      }

      public LetCommand CreateLetCommand()
      {
         var LetCommand = new LetCommand(_machine);
         _machine.Add(LetCommand);
         return LetCommand;
      }
   }
}
