namespace Yallocc
{
   public class BeginInterface<T> where T : struct
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BeginInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public ProduceInterFaceWithoutNameAndWithoutAction<T> Begin
      {
         get
         {
            _grammarBuilder.BeginGrammar();
            return new ProduceInterFaceWithoutNameAndWithoutAction<T>(_grammarBuilder);
         }
      }
   }
}
