using System.Collections.Generic;

namespace YalloccDemo.Basic
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

      public void Add(string name, BasicEntity variable)
      {
         if(string.IsNullOrEmpty(name))
         {
            return;
         }
         if(variable==null)
         {
            return;
         }
         _memory.Add(name, variable);
      }

      public bool Contaions(string name)
      {
         return _memory.ContainsKey(name);
      }

      public BasicEntity Get(string Name)
      {
         return _memory[Name];
      }

      public void Add(BasicCommand command)
      {
         _program.Add(command);
      }
   }
}
