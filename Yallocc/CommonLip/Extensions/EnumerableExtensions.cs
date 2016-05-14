using System;
using System.Collections.Generic;
using System.Linq;

namespace Yallocc.CommonLib
{
   public static class EnumerableExtensions
   {
      public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> elems, Func<T, bool> separator)
      {
         var iter = elems.GetEnumerator();
         while (iter.MoveNext())
         {
            if (!separator(iter.Current))
            {
               var segment = iter.Separate(separator).ToList();
               yield return segment;
            }
         }
      }

      private static IEnumerable<T> Separate<T>(this IEnumerator<T> iter, Func<T, bool> separator)
      {
         if (!separator(iter.Current))
         {
            do
            {
               yield return iter.Current;
            }
            while (iter.MoveNext() && (!separator(iter.Current)));
         }
      }
   }
}
