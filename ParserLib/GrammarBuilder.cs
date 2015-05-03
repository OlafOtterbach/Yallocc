using LexSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserLib
{
   public class GrammarBuilder<T>
   {
      private Transition _current;

      private List<Transition> _namedTransitions;

      private Transition _start;

      public GrammarBuilder()
      {
         _current = null;
         _namedTransitions = new List<Transition>();
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
         _namedTransitions.Clear();
      }

      public static GrammarBuilder<T> CreateGrammar()
      {
         return new GrammarBuilder<T>();
      }

      public GrammarBuilder<T> BeginGrammar()
      {
         _current = new Transition();
         return this;
      }

      public Transition EndGrammar()
      {
         ReplaceProxiesWithLabels();
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
         if(transition.Name != string.Empty)
         {
            _namedTransitions.Add(transition);
         }
      }

      private void ReplaceProxiesWithLabels()
      {
         var stack = new Stack<Transition>();
         var visited = new List<Transition>();
         stack.Push(_start);
         while(stack.Count() > 0)
         {
            var trans = stack.Pop();
            visited.Add(trans);
            var proxies = trans.Successors.OfType<ProxyTransition>().Cast<ProxyTransition>().ToList();
            var replaces = _namedTransitions.Where(x => proxies.Where(p => p.TargetName == x.Name).Any()).ToList();
            trans.RemoveSuccessors(proxies);
            trans.AddSuccessors(replaces);
            trans.Successors.Where(x => !visited.Contains(x)).ToList().ForEach(t => stack.Push(t));
         }
      }
   }
}
