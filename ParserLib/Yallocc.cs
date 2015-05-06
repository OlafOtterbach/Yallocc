// Yet Another Left Left One Compiler Complier
// <Author>Olaf Otterbach</Author>
// 2015
namespace ParserLib
{
   public class Yallocc<T>
   {
      private BuilderInterface<T> CreateBuilder()
      {
         var baseBuilder = new GrammarBuilder<T>();
         var builderInterface = new BuilderInterface<T>(baseBuilder);
         return builderInterface;
      }

   }
}
