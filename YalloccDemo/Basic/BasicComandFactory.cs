namespace YalloccDemo.Basic
{
   public class BasicComandFactory
   {
      private BasicEngine _machine;

      public BasicComandFactory()
      {
         _machine = new BasicEngine();
      }

      public DefCommand CreateDefCommand()
      {
         var defCommand = new DefCommand(_machine);
         _machine.Add(defCommand);
         return defCommand;
      }
   }
}
