using System;
using System.Collections.Generic;
using System.Linq;

namespace Yallocc.CommonLib
{
   public struct HeaderAndTail<T>
   {
      public T Header { get; set; }
      public IEnumerable<T> Tail { get; set; }
   }

   public static class EnumerableExtensions
   {
      public static IEnumerable<T> ToEnumerabele<T>(this T element)
      {
         yield return element;
      }

      public static HeaderAndTail<T> SplitHeaderAndTail<T>(this IEnumerable<T> elems)
      {
         if (elems == null) throw new ArgumentNullException("elems");

         var headerAndTail = new HeaderAndTail<T>();
         var iter = elems.GetEnumerator();
         if (iter.MoveNext())
         {
            headerAndTail.Header = iter.Current;
         }
         headerAndTail.Tail = SeparateTail(iter);

         return headerAndTail;
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
