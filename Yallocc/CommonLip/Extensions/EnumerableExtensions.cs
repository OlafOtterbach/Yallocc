using System;
using System.Collections.Generic;
using System.Linq;

namespace Yallocc.CommonLib
{
   public static class EnumerableExtensions
   {
      public static IEnumerable<T> ToEnumerabele<T>(this T element)
      {
         if (element == null) throw new ArgumentNullException("element");
         yield return element;
      }

      public static IEnumerable<IEnumerable<T>> SplitHeaderAndTail<T>(this IEnumerable<T> elems)
      {
         if (elems == null) throw new ArgumentNullException("elems");
         var iter = elems.GetEnumerator();
         if (iter.MoveNext())
         {
            var header = iter.Current;
            yield return header.ToEnumerabele();
         }
         var tail = SeparateTail(iter);
         yield return tail;
      }

      private static IEnumerable<T> SeparateTail<T>(this IEnumerator<T> iter)
      {
         while (iter.MoveNext())
         {
            yield return iter.Current;
         }
      }

      public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> elems, Func<T, bool> separator)
      {
         if (elems == null) throw new ArgumentNullException("elems");
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
