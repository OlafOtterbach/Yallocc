using System.Collections.Generic;
using System.Linq;

namespace Yallocc.Tokenizer
{
   public abstract class Tokenizer<T> where T : struct
   {
      private readonly Pattern<T>[] _patterns;
      private readonly T[] _ignoreTokenType;


      public Tokenizer(IEnumerable<Pattern<T>> patterns, IEnumerable<Pattern<T>> patternsToIgnore)
      {
         _patterns = patterns.Concat(patternsToIgnore).ToArray();
         _ignoreTokenType = patternsToIgnore.Select(p => p.TokenType).ToArray();
      }


      public abstract IEnumerable<Token<T>> Scan(string text);


      protected Pattern<T>[] Patterns
      {
         get
         {
            return _patterns;
         }
      }


      protected T[] IgnoreTokenType
      {
         get
         {
            return _ignoreTokenType;
         }
      }
   }
}
