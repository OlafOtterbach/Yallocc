using System;
using System.Linq;

namespace ParserLib
{
   public class ProducerInterface<T>
   {
      private GrammarBuilder<T> _grammarBuilder;

      public ProducerInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public Transition EndGrammar()
      {
         return _grammarBuilder.EndGrammar();
      }

      public ProducerInterface<T> AddToken(T tokenType)
      {
         _grammarBuilder.AddToken(tokenType);
         return this;
      }

      public ProducerInterface<T> AddToken(T tokenType, Action action)
      {
         _grammarBuilder.AddToken(tokenType, action);
         return this;
      }

      public ProducerInterface<T> AddToken(string name, T tokenType)
      {
         _grammarBuilder.AddToken(name, tokenType);
         return this;
      }

      public ProducerInterface<T> AddToken(string name, T tokenType, Action action)
      {
         _grammarBuilder.AddToken(name, tokenType, action);
         return this;
      }

      public ProducerInterface<T> AddLabel(string label)
      {
         _grammarBuilder.AddLabel(label);
         return this;
      }

      public ProducerInterface<T> AddLabel(string label, Action action)
      {
         _grammarBuilder.AddLabel(label, action);
         return this;
      }

      public ProducerInterface<T> AddSubGrammar(Transition subGrammar)
      {
         _grammarBuilder.AddSubGrammar(subGrammar);
         return this;
      }

      public ProducerInterface<T> AddSubGrammar(Transition subGrammar, Action action)
      {
         _grammarBuilder.AddSubGrammar(subGrammar, action);
         return this;
      }

      public ProducerInterface<T> AddSubGrammar(string name, Transition subGrammar)
      {
         _grammarBuilder.AddSubGrammar(name, subGrammar);
         return this;
      }

      public ProducerInterface<T> AddSubGrammar(string name, Transition subGrammar, Action action)
      {
         _grammarBuilder.AddSubGrammar(name, subGrammar, action);
         return this;
      }

      public ProducerInterface<T> GotoLabel(string label)
      {
         _grammarBuilder.GotoLabel(label);
         return this;
      }

      public ProducerInterface<T> Switch(params BranchInterface<T>[] branches)
      {
         _grammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return this;
      }
   }
}
