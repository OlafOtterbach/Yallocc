namespace Yallocc
{
   public class BeginInterface<T>
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BeginInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public ProduceInterFaceWithoutNameAndActionAttribute<T> Begin
      {
         get
         {
            return new ProduceInterFaceWithoutNameAndActionAttribute<T>(_grammarBuilder);
         }
      }
   }
}
