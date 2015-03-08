using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LexSharp
{
   public class Lex
   {
      public Lex()
      {
         _patterns = new List<Pattern>();
      }

      public void Register(Pattern pattern)
      {
         _patterns.Add(pattern);
      }

      public IEnumerable<Token> Scan(string text)
      {
         var matchesOfPatterns = _patterns.SelectMany(p => p.TokenPattern
                                                        .Matches(text)
                                                        .Cast<Match>()
                                                        .Select(m => new Token(m.Value, p.TokenType, m.Index, m.Length))
                                                     )
                                          .GroupBy(t => t.Index)
                                          .Select(g => g.ToList())
                                          .Select(g => g.First(x => x.Length == g.Max(y => y.Length)))
                                          ;

         return null;
      }

      private List<Pattern> _patterns;
   }
}
