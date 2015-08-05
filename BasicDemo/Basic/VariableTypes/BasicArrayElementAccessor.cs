using System.Collections.Generic;
using System.Linq;

namespace BasicDemo.Basic
{
   public class BasicArrayElementAccessor : BasicEntity
   {
      private BasicArray _array;
      private List<ExpressionCommand> _indexEvaluations;

      public BasicArrayElementAccessor(BasicArray array)
      {
         _array = array;
         _indexEvaluations = new List<ExpressionCommand>();
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
            return _array.Get(indices);
         }
      }
   }
}
