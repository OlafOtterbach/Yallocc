/// <summary>YALLOCC</summary>
/// <summary>[Y}et [A]nother [L]eft [L]eft [O]ne [C]ompiler [C]omplier.</summary>
/// <author>Olaf Otterbach</author>
/// <date>11.05.2015</date>

using LexSharp;

namespace Yallocc
{
   public class Yallocc<T> where T : struct
   {
      LexSharp<T> _lex;
      BuilderInterface<T> _builderInterface;
      GrammarDictionary _grammers;


      public Yallocc()
      {
         _grammers = new GrammarDictionary();
         var baseBuilder = new GrammarBuilder<T>(_grammers);
         _builderInterface = new BuilderInterface<T>(baseBuilder);
         _lex = new LexSharp<T>();
      }

      public void AddToken(string patternText, T tokenType)
      {
         _lex.Register(patternText, tokenType);
      }

      public EnterInterface<T> Grammar(string name)
      {

         return _builderInterface.Grammar(name);
      }

      public EnterInterface<T> MasterGrammar(string name)
      {
         return _builderInterface.MasterGrammar(name);
      }

      public BranchInterface<T> Branch
      {
         get
         {
            return _builderInterface.Branch;
         }
      }

      public YParser<T> CreateParser()
      {
         if(!_lex.IsComplete())
         {
            throw new MissingTokenDefinitionException("Not all types of tokens are defined.");
         }

         if (GrammarInitialisationAndValidation.AnyProxyTransitions(_grammers))
         {
            throw new GrammarBuildingException("Not all subgrammars are defined") { HasUndefinedSubgrammars = true };
         }

         if(!_grammers.HasMasterGrammar())
         {
            throw new GrammarBuildingException("Master grammar is not defined.") { MasterGrammarIsNotDefined = true };
         }

         GrammarInitialisationAndValidation.ReplaceProxiesInGrammarTransitions(_grammers);
         
         return new YParser<T>(_grammers.GetMasterGrammar(), _lex);
      }
   }
}
