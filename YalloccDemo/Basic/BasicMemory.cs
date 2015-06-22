using System.Collections.Generic;

namespace YalloccDemo.Basic
{
   public class BasicMemory
   {
      private Dictionary<string, BasicType> _memory;

      public BasicMemory()
      {
         _memory = new Dictionary<string, BasicType>();
      }

      public void Add(string name, BasicType variable)
      {
         _memory.Add(name, variable);
      }
   }
}
