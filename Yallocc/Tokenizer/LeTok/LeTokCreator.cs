namespace Yallocc.Tokenizer.LeTok
{
   public class LeTokCreator<T> : TokenizerCreator<T> where T :struct
   {
      public override Tokenizer<T> Create()
      {
         return new LeTok<T>(Patterns, PatternsToIgnore);
      }
   }
}
