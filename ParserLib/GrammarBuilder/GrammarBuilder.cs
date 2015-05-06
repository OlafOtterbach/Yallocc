using LexSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserLib
{
   public class GrammarBuilder<T>
   {
      private Transition _current;

      private Transition _start;

      public GrammarBuilder()
      {
         _current = null;
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

      public GrammarBuilder<T> BeginGrammar()
      {
         _current = new Transition();
         return this;
      }

      public Transition EndGrammar()
      {
         var inititialisatorAndValidator = new GrammarInitialisationAndValidation();
         inititialisatorAndValidator.ReplaceAndValidateProxiesWithLabels(_start);
         return _start;
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

      public void AddSubGrammar(Transition subGrammar)
      {
         var grammarTransition = new GrammarTransition(subGrammar);
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

      private struct TransitionProxyPair
      {
         public TransitionProxyPair(Transition transition, Transition proxy) : this()
         {
            Transition = transition;
            Proxy = proxy;
         }
         public Transition Transition { get; set; }
         public Transition Proxy { get; set; }
      }
   }
}
