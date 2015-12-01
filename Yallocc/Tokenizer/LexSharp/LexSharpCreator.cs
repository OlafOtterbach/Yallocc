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
