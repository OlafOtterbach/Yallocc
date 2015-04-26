using System;
using System.Linq;

namespace ParserLib
{
   public class BranchInterface<T>
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BranchInterface()
      {
         _grammarBuilder = new GrammarBuilder<T>();
      }

      public Transition First
      {
         get
         {
            return _grammarBuilder.Start;
         }
      }

      public Transition Last
      {
         get
         {
            return _grammarBuilder.Current;
         }
      }

      public BranchInterface<T>Token(T tokenType)
      {
         _grammarBuilder.AddToken(tokenType);
         return this;
      }

      public BranchInterface<T>Token(T tokenType, Action action)
      {
         _grammarBuilder.AddToken(tokenType, action);
         return this;
      }

      public BranchInterface<T>Token(string name, T tokenType)
      {
         _grammarBuilder.AddToken(name, tokenType);
         return this;
      }

      public BranchInterface<T>Token(string name, T tokenType, Action action)
      {
         _grammarBuilder.AddToken(name, tokenType, action);
         return this;
      }

      public BranchInterface<T> Label(string label)
      {
         _grammarBuilder.AddLabel(label);
         return this;
      }

      public BranchInterface<T> Label(string label, Action action)
      {
         _grammarBuilder.AddLabel(label,action);
         return this;
      }

      public BranchInterface<T> AddSubGrammar(Transition subGrammar)
      {
         _grammarBuilder.AddSubGrammar(subGrammar);
         return this;
      }

      public BranchInterface<T> AddSubGrammar(Transition subGrammar, Action action)
      {
         _grammarBuilder.AddSubGrammar(subGrammar, action);
         return this;
      }

      public BranchInterface<T> AddSubGrammar(string name, Transition subGrammar)
      {
         _grammarBuilder.AddSubGrammar(name, subGrammar);
         return this;
      }

      public BranchInterface<T> AddSubGrammar(string name, Transition subGrammar, Action action)
      {
         _grammarBuilder.AddSubGrammar(name, subGrammar, action);
         return this;
      }

      public BranchInterface<T> Goto(string label)
      {
         _grammarBuilder.GotoLabel(label);
         return this;
      }

      public BranchInterface<T> Switch(params BranchInterface<T>[] branches)
      {
         _grammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return this;
      }

      internal GrammarBuilder<T> GrammarBuilder
      {
         get
         {
            return _grammarBuilder;
         }
      }
   }
}
