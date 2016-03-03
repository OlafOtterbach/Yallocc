/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

using System.Linq;
using Yallocc.Tokenizer;

namespace Yallocc
{
   public class ParserResult<T> where T : struct
   {
      private enum ParsRes
      {
         success = 0,
         grammar_of_text_not_complete = 1,
         syntax_error = 2
      }

      private ParsRes _result;

      public bool Success
      {
         get
         {
            return _result == ParsRes.success;
         }
      }

      public bool GrammarOfTextNotComplete
      {
         get
         {
            return _result == ParsRes.grammar_of_text_not_complete;
         }
         set
         {
            _result = value ? (_result | ParsRes.grammar_of_text_not_complete) : (_result & (~ParsRes.grammar_of_text_not_complete));
         }
      }

      public bool SyntaxError
      {
         get
         {
            return _result == ParsRes.syntax_error;
         }
         set
         {
            _result = value ? (_result | ParsRes.syntax_error) : (_result & (~ParsRes.syntax_error));
         }
      }

      public int Position { get; set; }

      public Token<T> FailedToken { get; set; }
   }
}
