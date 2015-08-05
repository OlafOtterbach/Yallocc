using System.Collections.Generic;

namespace BasicDemo.Basic
{
   public class BasicEngine
   {
      private Dictionary<string, BasicEntity> _memory;

      private List<BasicCommand> _program;

      public BasicEngine()
      {
         _memory = new Dictionary<string, BasicEntity>();
         _program = new List<BasicCommand>();
      }

      public void RegisterVariable(string name, BasicEntity variable)
      {
         if(string.IsNullOrEmpty(name))
         {
            return;
         }
         if(variable==null)
         {
            return;
         }
         _memory[name] = variable;
      }

      public BasicEntity GetVariable(string Name)
      {
         return _memory[Name];
         // ToDo: ExceptionUnknownVariable
      }

      public void Add(BasicCommand command)
      {
         _program.Add(command);
      }
   }
}
