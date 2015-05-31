using LexSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Yallocc
{
   public class GrammarBuilder<T>
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

      public GrammarBuilder<T> CreateBranchBuilder()
      {
         return new GrammarBuilder<T>(_grammars);
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
            throw new GrammarBuildingException("Grammar with this name already defined.") { HasAlreadyExistingGrammarName = true };
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

      public GrammarBuilder<T> BeginGrammar()
      {
         _current = new Transition();
         return this;
      }

      public void EndGrammar()
      {
         if(_start == null)
         {
            AddTransition(new DefaultGrammarTransition());
         }
         _grammars.AddGrammar(_name, _start);
         GrammarInitialisationAndValidation.ReplaceAndValidateProxiesWithLabels(_start);
         GrammarInitialisationAndValidation.ReplaceProxiesInGrammarTransitions(_grammars);
      }

      public void AddName(string name)
      {
         _current.Name = name;
      }

      public void AddAction(Action action)
      {
         if( _current is ActionTransition)
         {
            (_current as ActionTransition).Action = action;
         }
         else
         {
            throw new GrammarBuildingException("Transition can not get an action.") { HasSettedActionInNonActionTransition = true };
         }
      }

      public void AddAction(Action<Token<T>> action)
      {
         if(_current is TokenTypeTransition<T>)
         {
            (_current as TokenTypeTransition<T>).Action = action;
         }
         else
         {
            throw new GrammarBuildingException("Transition can not get an action.") { HasSettedActionInNonActionTransition = true };
         }
      }

      public void AddToken(T tokenType)
      {
         var tokenTransition = new TokenTypeTransition<T>(tokenType);
         AddTransition(tokenTransition);
      }

      public void AddLabel(string label)
      {
         var labelTransition = new LabelTransition(label);
         AddTransition(labelTransition);
      }

      public void AddLambda(Action action)
      {
         var transition = new ActionTransition(action);
         AddTransition(transition);
      }

      public void AddLambda()
      {
         var transition = new DefaultGrammarTransition();
         AddTransition(transition);
      }

      public void AddSubGrammar(string nameOfSubGrammar)
      {
         var proxyTransition = new ProxyTransition(nameOfSubGrammar);
         var grammarTransition = new GrammarTransition(proxyTransition);
         AddTransition(grammarTransition);
      }

      public void GotoLabel(string label)
      {
         var proxyTransition = new ProxyTransition(label);
         AddTransition(proxyTransition);
      }

      public void Switch(params GrammarBuilder<T>[] branches)
      {
         if(_start == null)
         {
            AddTransition(new DefaultGrammarTransition());
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
