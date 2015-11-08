using System.Linq;
using System.Collections.Generic;

namespace BasicDemo.Basic
{
   public class BasicArray : BasicEntity
   {
      private BasicEntity[] _array;
      private int[] _dimensions;

      public BasicArray(params int[] dimensions)
      {
         _dimensions = dimensions.Select(dim => (dim > 0) ? dim : 1).ToArray();
         var size = dimensions.Aggregate((current, dim) => current = current * dim);
         _array = new BasicEntity[size];
      }

      public override BasicType Type
      {
         get
         {
            return BasicType.e_array;
         }
      }

      public BasicEntity Get(params int[] indices)
      {
         if (!Validate(indices))
         {
            throw new BasicOutOfRangeException("Out of range error.");
         }
         return _array[GetIndex(indices, _dimensions)];
      }

      public void Set(BasicEntity value, params int[] indices)
      {
         if (!Validate(indices))
         {
            throw new BasicOutOfRangeException("Out of range error.");
         }
         if (value is BasicVariable);
         {
            var variable = value as BasicVariable;
            var elem = _array[GetIndex(indices, _dimensions)];
            if (elem != null)
            {
               var varElem = elem as BasicVariable;
               varElem.Set(value);
            }
            else
            {
               _array[GetIndex(indices, _dimensions)] = variable.Clone();
            }
         }
      }

      public int Dimensions
      {
         get
         {
            return _dimensions.Length;
         }
      }

      public int DimensionSize(int dim)
      {
         return _dimensions[dim];
      }

      private bool Validate(int[] indices)
      {
         var isValid = _dimensions.Zip(indices, (dim, index) => (index >= 0) && (index < dim)).All(result => result)
                       && (indices.Count() == _dimensions.Count());
         return isValid;
      }

      private static int GetIndex(int[] indices, int[] dimensions)
      {
         var factors = (new List<int> { 1 }).Concat(dimensions.Reverse().Skip(1).Reverse()).ToList();
         var commulatedFactors = factors.Select((x, i) => (i<=1) ? x : factors.GetRange(0, i+1).Aggregate((current, dim) => current = current * dim)).ToList();
         var pairs = indices.Select((index, pos) => new { Index = index, Pos = pos }).ToList();
         var resultIndex = indices.Select((index, pos) => new { Index = index, Pos = pos })
                                  .Select(pair => pair.Index * commulatedFactors[pair.Pos])
                                  .Aggregate((current, x) => current = current + x);
         return resultIndex;
      }
   }
}
