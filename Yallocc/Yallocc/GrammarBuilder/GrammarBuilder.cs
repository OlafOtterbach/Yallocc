using System;
using System.Linq;
using Yallocc.Tokenizer;

namespace Yallocc
{
   public class GrammarBuilder<TCtx,T> where T : struct
   {
      private GrammarDictionary _grammars;

      private Transition _current;

      private Transition _start;

      private string _name;

      public GrammarBuilder(GrammarDictionary grammars)
      {
         _grammars = grammars;
         _start = null;
         _current = _start;
      }

      public GrammarBuilder<TCtx,T> CreateBranchBuilder()
      {
         return new GrammarBuilder<TCtx,T>(_grammars);
      }

      public Transition Start
      {
         get
         {
            return _start;
         }
      }

      public Transition Current
      {
         get
         {
            return _current;
         }
      }

      public void Reset()
      {
         _start = null;
         _current = null;
      }

      public void CreateGrammar(string name)
      {
         Reset();
         if(_grammars.Contains(name))
         {
            throw new GrammarBuildingException(string.Format("Grammar with name {0} already defined.", name)) { HasAlreadyExistingGrammarName = true };
         }
         _name = name;
      }

      public void CreateMasterGrammar(string name)
      {
         if (_grammars.MasterGrammar == string.Empty)
         {
            CreateGrammar(name);
            _grammars.MasterGrammar = name;
         }
         else
         {
            throw new GrammarBuildingException("Master grammar already defined.") {MasterGrammarAlreadyDefined = true};
         }
      }

      public GrammarBuilder<TCtx,T> EnterGrammar()
      {
         AddLambda();
         return this;
      }

      public void EndGrammar()
      {
         if(_start == null)
         {
            AddTransition(new Transition());
         }
         _grammars.AddGrammar(_name, _start);
         GrammarInitialisationAndValidation<TCtx>.ReplaceAndValidateProxiesWithLabels(_start);
         GrammarInitialisationAndValidation<TCtx>.ReplaceProxiesInGrammarTransitions(_grammars);
      }

      public void AddName(string name)
      {
         _current.Name = name;
      }

      public void AddAction(Action<TCtx> action)
      {
         if( _current is ActionTransition<TCtx>)
         {
            (_current as ActionTransition<TCtx>).Action = action;
         }
         else if(_current is TokenTypeTransition<TCtx, T>)
         {
            (_current as TokenTypeTransition<TCtx, T>).Action
               = (TCtx ctx, Token<T> token) => action(ctx);
         }
         else
         {
            throw new GrammarBuildingException("Transition can not get an action.") { HasSettedActionInNonActionTransition = true };
         }
      }

      public void AddAction(Action<TCtx, Token<T>> action)
      {
         if(_current is TokenTypeTransition<TCtx,T>)
         {
            (_current as TokenTypeTransition<TCtx,T>).Action = action;
         }
         else
         {
            throw new GrammarBuildingException("Transition can not get an action.") { HasSettedActionInNonActionTransition = true };
         }
      }

      public void AddToken(T tokenType)
      {
         AddTransition(new TokenTypeTransition<TCtx, T>(tokenType));
      }

      public void AddAnyToken()
      {
         AddTransition(new AnyTokenTypeTransition<TCtx, T>());
      }

      public void AddLabel(string label)
      {
         var labelTransition = new LabelTransition<TCtx>(label);
         AddTransition(labelTransition);
      }

      public void AddLambda()
      {
         var transition = new ActionTransition<TCtx>();
         AddTransition(transition);
      }

      public void AddSubGrammar(string nameOfSubGrammar)
      {
         var proxyTransition = new ProxyTransition(nameOfSubGrammar);
         var grammarTransition = new GrammarTransition<TCtx>(proxyTransition);
         AddTransition(grammarTransition);
      }

      public void GotoLabel(string label)
      {
         var proxyTransition = new ProxyTransition(label);
         AddTransition(proxyTransition);
      }

      public void Switch(params GrammarBuilder<TCtx,T>[] branches)
      {
         if(_start == null)
         {
            AddTransition(new ActionTransition<TCtx>());
         }
         branches.ToList().ForEach(x => _current.AddSuccessor(x.Start));
         var branchEndTransition = new Transition();
         branches.Where(b => !(b.Current is ProxyTransition)).ToList().ForEach(x => x.Current.AddSuccessor(branchEndTransition));
         _current = branchEndTransition;
      }

      private void AddTransition(Transition transition)
      {
         if (_start == null)
         {
            _start = transition;
            _current = _start;
         }
         else
         {
            _current.AddSuccessor(transition);
            _current = transition;
         }
      }
   }
}
