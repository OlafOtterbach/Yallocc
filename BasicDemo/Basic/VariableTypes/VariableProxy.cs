namespace BasicDemo.Basic
{
   public class VariableProxy : BasicEntity
   {
      private BasicEngine _engine;
      private string _name;

      public VariableProxy(BasicEngine engine, string name)
      {
         _engine = engine;
         _name = name;
      }

      public override BasicType Type
      {
         get
         {
            var variable = _engine.GetVariable(_name) as BasicVariable;
            return variable.Type;
         }
      }

      public BasicEntity Value
      {
         get 
         {
            var variable = _engine.GetVariable(_name);
            return variable;
         }
      }
   }
}