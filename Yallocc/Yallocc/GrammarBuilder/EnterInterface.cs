namespace Yallocc
{
   public class EnterInterface<T> where T : struct
   {
      private GrammarBuilder<T> _grammarBuilder;

      public EnterInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public ProduceInterfaceWithNameAndWithAction<T> Enter
      {
         get
         {
            _grammarBuilder.EnterGrammar();
            return new ProduceInterfaceWithNameAndWithAction<T>(_grammarBuilder);
         }
      }
   }
}
