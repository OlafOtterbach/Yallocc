/// <summary>YALLOCC</summary>
/// <summary>[Y}et [A]nother [L]eft [L]eft [O]ne [C]ompiler [C]omplier.</summary>
/// <author>Olaf Otterbach</author>
/// <date>11.05.2015</date>

namespace ParserLib
{
   public class Yallocc<T>
   {
      BuilderInterface<T> _builderInterface;

      public Yallocc()
      {
         var baseBuilder = new GrammarBuilder<T>();
         _builderInterface = new BuilderInterface<T>(baseBuilder);
      }

      public BeginInterface<T> CreateGrammar()
      {
         return _builderInterface.CreateGrammar();
      }

      public BranchInterFaceWithoutNameAndActionAttribute<T> Branch
      {
         get
         {
            return _builderInterface.Branch;
         }
      }

      public YParser CreateParser(YGrammar grammar)
      {
         return new YParser(grammar);
      }
   }
}
