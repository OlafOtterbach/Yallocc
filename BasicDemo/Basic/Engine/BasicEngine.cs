using System.Collections.Generic;

namespace BasicDemo.Basic
{
   public class BasicEngine
   {
      private Dictionary<string, BasicEntity> _memory;

      private List<BasicCommand> _program;

      private BasicCursor _cursor;

      public BasicEngine()
      {
         _memory = new Dictionary<string, BasicEntity>();
         _program = new List<BasicCommand>();
         _cursor = new BasicCursor(this);
      }

      public IEnumerable<BasicCommand> Program
      {
         get
         {
            return _program;
         }
      }

      public Dictionary<string, BasicEntity> Memory
      {
         get
         {
            return _memory;
         }
      }

      public BasicCursor Cursor
      {
         get
         {
            return _cursor;
         }
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
         if(_memory.ContainsKey(name))
         {
            throw new BasicVariableAlreadyDefinedException();
         }
         _memory[name] = variable;
      }

      public bool HasVariable(string name)
      {
         if (string.IsNullOrEmpty(name))
         {
            return false;
         }

         return _memory.ContainsKey(name);
      }

      public BasicEntity GetVariable(string name)
      {
         if (string.IsNullOrEmpty(name))
         {
            return null;
         }
         return HasVariable(name) ? _memory[name] : null;
      }

      public void Add(BasicCommand command)
      {
         _program.Add(command);
      }

      public void Run()
      {
         _cursor.Reset();
         while(!_cursor.EndOfProgram)
         {
            _cursor.Current.Execute();
         }
      }
   }
}
