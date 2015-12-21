/// <summary>YALLOCC</summary>
/// <summary>[Y}et [A]nother [L]eft [L]eft [O]ne [C]ompiler [C]omplier.</summary>
/// <author>Olaf Otterbach</author>
/// <date>11.05.2015</date>

using Yallocc.Tokenizer;

namespace Yallocc
{
   public class Yallocc<TCtx,T> where T : struct
   {
      private readonly TokenizerCreator<T> _tokenizerCreator;
      private readonly GrammarBuilderInterface<TCtx, T> _builderInterface;
      private readonly GrammarDictionary _grammers;
      
      public Yallocc(TokenizerCreator<T> tokenizerCreator)
      {
         _grammers = new GrammarDictionary();
         var baseBuilder = new GrammarBuilder<TCtx, T>(_grammers);
         _builderInterface = new GrammarBuilderInterface<TCtx, T>(baseBuilder);
         _tokenizerCreator = tokenizerCreator;
         TokenCompletenessIsChecked = true;
      }

      public TokenPatternBuilder<T> DefineTokens()
      {
         return new TokenPatternBuilder<T>(_tokenizerCreator);
      }

      public EnterInterface<TCtx, T> Grammar(string name)
      {

         return _builderInterface.Grammar(name);
      }

      public EnterInterface<TCtx, T> MasterGrammar(string name)
      {
         return _builderInterface.MasterGrammar(name);
      }

      public BranchInterface<TCtx, T> Branch
      {
         get
         {
            return _builderInterface.Branch;
         }
      }

      public bool TokenCompletenessIsChecked { get; set; }

      public ParserAndTokenizer<TCtx,T> CreateParser()
      {
         if(TokenCompletenessIsChecked && (!_tokenizerCreator.IsComplete()))
         {
            throw new MissingTokenDefinitionException("Not all types of tokens are defined.");
         }

         if (GrammarInitialisationAndValidation<TCtx>.AnyProxyTransitions(_grammers))
         {
            throw new GrammarBuildingException("Not all subgrammars are defined") { HasUndefinedSubgrammars = true };
         }

         if(!_grammers.HasMasterGrammar())
         {
            throw new GrammarBuildingException("Master grammar is not defined.") { MasterGrammarIsNotDefined = true };
         }

         GrammarInitialisationAndValidation<TCtx>.ReplaceProxiesInGrammarTransitions(_grammers);
         var tokenizer = _tokenizerCreator.Create();
         var parser = new Parser<TCtx,T>(_grammers.GetMasterGrammar());
         return new ParserAndTokenizer<TCtx,T>(parser, tokenizer);
      }
   }
}
