using System.Linq;

namespace BasicDemo.Basic
{
   public class BasicCursor
   {
      private BasicCommand _current;
      private BasicEngine _engine;

      public BasicCursor(BasicEngine engine)
      {
         _engine = engine;
      }

      public BasicCommand Current
      {
         get
         {
            return _current;
         }
      }

      public bool EndOfProgram
      {
         get
         {
            return (_current == _engine.Program.Last());
         }
      }

      public void Reset()
      {
         _current = _engine.Program.First();
      }

      public void Next()
      {
         if(!EndOfProgram)
         {
            _current = _engine.Program.SkipWhile(x => x != _current).Skip(1).First();
         }
      }

      public void GotoCommand(BasicCommand command)
      {
         _current = _engine.Program.SkipWhile(x => x != command).First();
      }

      public void GotoNextOfCommand(BasicCommand command)
      {
         _current = _engine.Program.SkipWhile(x => x != command).First();
         Next();
      }
   }
}
