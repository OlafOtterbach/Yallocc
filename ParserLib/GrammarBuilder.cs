using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserLib
{
   public class GrammarBuilder<T>
   {
      private struct Branch
      {
         public Branch(Transition start) : this()
         {
            Start = start;
            EndTransitions = new List<Transition>();
         }

         public Transition Start { get; set; }

         public List<Transition> EndTransitions { get; set; }
      }

      private Stack<Branch> _contextStack = new Stack<Branch>();

      private Transition _current;

      private List<Transition> _namedTransitions;

      public GrammarBuilder()
      {
         _current = null;
         _namedTransitions = new List<Transition>();
      }

      public static GrammarBuilder<T> CreateGrammar()
      {
         return new GrammarBuilder<T>();
      }

      public GrammarBuilder<T> BeginGrammar()
      {
         _current = new Transition();
         BeginSwitch();
         return this;
      }

      public Transition EndGrammar()
      {
         EndSwitch();
         return _current;
      }

      public GrammarBuilder<T> AddToken(T tokenType)
      {
         var tokenTransition = new TokenTypeTransition<T>(tokenType);
         AddTransition(tokenTransition);
         return this;
      }

      public GrammarBuilder<T> AddToken(T tokenType, Action action)
      {
         var tokenTransition = new TokenTypeTransition<T>(tokenType, action);
         AddTransition(tokenTransition);
         return this;
      }

      public GrammarBuilder<T> AddToken(string name, T tokenType)
      {
         var tokenTransition = new TokenTypeTransition<T>(name, tokenType);
         AddTransition(tokenTransition);
         return this;
      }

      public GrammarBuilder<T> AddToken(string name, T tokenType, Action action)
      {
         var tokenTransition = new TokenTypeTransition<T>(name, tokenType, action);
         AddTransition(tokenTransition);
         return this;
      }

      public GrammarBuilder<T> AddLabel(string label)
      {
         var labelTransition = new LabelTransition(label);
         AddTransition(labelTransition);
         return this;
      }

      public GrammarBuilder<T> AddLabel(string label, Action action)
      {
         var labelTransition = new LabelTransition(label, action);
         AddTransition(labelTransition);
         return this;
      }

      public GrammarBuilder<T> AddSubGrammar(Transition subGrammar)
      {
         var grammarTransition = new GrammarTransition(subGrammar);
         AddTransition(grammarTransition);
         return this;
      }

      public GrammarBuilder<T> AddSubGrammar(Transition subGrammar, Action action)
      {
         var grammarTransition = new GrammarTransition(subGrammar, action);
         AddTransition(grammarTransition);
         return this;
      }

      public GrammarBuilder<T> AddSubGrammar(string name, Transition subGrammar)
      {
         var grammarTransition = new GrammarTransition(name, subGrammar);
         AddTransition(grammarTransition);
         return this;
      }

      public GrammarBuilder<T> AddSubGrammar(string name, Transition subGrammar, Action action)
      {
         var grammarTransition = new GrammarTransition(name, subGrammar, action);
         AddTransition(grammarTransition);
         return this;
      }

      public GrammarBuilder<T> GotoLabel(string label)
      {
         var labels = _namedTransitions.Where(x => x.Name == label);
         if (labels.Any())
         {
            var targetTransition = labels.First();
            _current.AddSuccessor(targetTransition);
            _current = _contextStack.Peek().Start;
         }
         else
         {
            // Throw Not Found Exception
         }
         return this;
      }

      public GrammarBuilder<T> BeginSwitch()
      {
         _contextStack.Push(new Branch(_current));
         return this;
      }

      public GrammarBuilder<T> CreateBranch()
      {
         var branch = _contextStack.Peek();
         branch.EndTransitions.Add(_current);
         _current = branch.Start;
         return this;
      }

      public GrammarBuilder<T> EndSwitch()
      {
         var branch = _contextStack.Pop();
         var endTransition = new Transition();
         branch.EndTransitions.Where(t => !t.Successors.Any()).ToList().ForEach(t => t.AddSuccessor(endTransition));
         return this;
      }

      private void AddTransition(Transition transition)
      {
         _current.AddSuccessor(transition);
         _current = transition;
         if(transition.Name != string.Empty)
         {
            _namedTransitions.Add(transition);
         }
      }
   }
}
