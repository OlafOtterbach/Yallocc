using System.Collections.Generic;
using System.Linq;

namespace Yallocc.Tokenizer
{
   public abstract class Tokenizer<T> where T : struct
   {
      private readonly IEnumerable<Pattern<T>> _patterns;
      private readonly IEnumerable<T> _ignoreTokenType;


      public Tokenizer(IEnumerable<Pattern<T>> patterns, IEnumerable<Pattern<T>> patternsToIgnore)
      {
         _patterns = patterns.Concat(patternsToIgnore).ToList();
         _ignoreTokenType = patternsToIgnore.Select(p => p.TokenType).ToList();
      }


      public abstract IEnumerable<Token<T>> Scan(string text);


      protected IEnumerable<Pattern<T>> Patterns
      {
         get
         {
            return _patterns;
         }
      }


      protected IEnumerable<T> IgnoreTokenType
      {
         get
         {
            return _ignoreTokenType;
         }
      }
   }
}
