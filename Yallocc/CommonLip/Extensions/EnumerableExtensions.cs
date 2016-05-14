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
         var segment = iter.Separate(separator).ToList();
         while(segment.Any())
         {
               yield return segment;
               segment = iter.Separate(separator).ToList();
         }
      }

      public static IEnumerable<IEnumerable<T>> Split2<T>(this IEnumerable<T> elems, Func<T, bool> separator)
      {
         var iter = elems.GetEnumerator();
         while (iter.MoveNext())
         {
            if (!separator(iter.Current))
            {
               var segment = iter.Separate2(separator).ToList();
               yield return segment;
            }
         }
      }

      private static IEnumerable<T> Separate2<T>(this IEnumerator<T> iter, Func<T, bool> separator)
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

      private static IEnumerable<T> Separate<T>(this IEnumerator<T> iter, Func<T, bool> separator)
      {
         while(iter.MoveNext() && (!separator(iter.Current)))
         {
            yield return iter.Current;
         }
      }
   }
}
