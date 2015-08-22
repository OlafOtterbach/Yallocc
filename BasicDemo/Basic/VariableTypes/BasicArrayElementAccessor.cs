using System.Collections.Generic;
using System.Linq;

namespace BasicDemo.Basic
{
   public class BasicArrayElementAccessor : BasicEntity
   {
      private BasicEngine _engine;
      private string _name;
      private BasicArray _array;
      private List<ExpressionCommand> _indexEvaluations;

      public BasicArrayElementAccessor(BasicArray array, params ExpressionCommand[] expressions)
      {
         _name = null;
         _engine = null;
         _array = array;
         _indexEvaluations = expressions.ToList();
      }

      public BasicArrayElementAccessor(BasicEngine engine, string name, params ExpressionCommand[] expressions)
      {
         _array = null;
         _engine = engine;
         _name = name;
         _indexEvaluations = expressions.ToList();
      }

      public override BasicType Type
      {
         get
         {
            return BasicType.e_array;
         }
      }

      public void Add(ExpressionCommand expression)
      {
         _indexEvaluations.Add(expression);
      }

      public BasicEntity Value
      {
         get
         {
            var indices = _indexEvaluations.Select(cmd => cmd.Execute())
                                           .Select(x => x.IsArray ? (x as BasicArrayElementAccessor).Value : x)
                                           .OfType<BasicInteger>()
                                           .Select(intVar => intVar.Value)
                                           .ToArray();
            var array = (_name == null) ? _array : _engine.GetVariable(_name) as BasicArray;
            return array.Get(indices);
         }
      }
   }
}
