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

      private Transition __start;

      private Transition _start
      {
         get
         {
            return __start;
         }
         set
         {
            __start = value;
         }
      }

      private string _name;

      public GrammarBuilder(GrammarDictionary grammars)
      {
         _grammars = grammars;
         _current = null;
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
            //error;
         }
         _name = name;
      }

      public void CreateMasterGrammar(string name)
      {
         CreateGrammar(name);
         _grammars.MasterGrammar = name;
      }

      public GrammarBuilder<T> BeginGrammar()
      {
         _current = new Transition();
         return this;
      }

      public void EndGrammar()
      {
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
            //error
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
            //error
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
         branches.ToList().ForEach(x => _current.AddSuccessor(x.Start));
         var branchEndTransition = new Transition();
         branches.Where(b => !(b is ProxyTransition)).ToList().ForEach(x => x.Current.AddSuccessor(branchEndTransition));
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
