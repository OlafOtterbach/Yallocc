/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>
namespace Yallocc
{
   public class EnterInterface<TCtx,T> where T : struct
   {
      private GrammarBuilder<TCtx,T> _grammarBuilder;

      public EnterInterface(GrammarBuilder<TCtx,T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public ProduceInterfaceWithNameAndWithAction<TCtx, T> Enter
      {
         get
         {
            _grammarBuilder.EnterGrammar();
            return new ProduceInterfaceWithNameAndWithAction<TCtx,T>(_grammarBuilder);
         }
      }
   }
}
