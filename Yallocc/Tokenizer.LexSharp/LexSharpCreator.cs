/// <summary>Tokenizer</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

namespace Yallocc.Tokenizer.LexSharp
{
   public class LexSharpCreator<T> : TokenizerCreator<T> where T : struct
   {
      public override Tokenizer<T> Create()
      {
         return new LexSharp<T>(Patterns, PatternsToIgnore);
      }
   }
}
