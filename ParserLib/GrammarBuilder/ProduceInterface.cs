using System;
using System.Linq;

namespace ParserLib
{
   public class ProduceInterface<T>
   {
      protected ProduceInterface(GrammarBuilder<T> grammarBuilder)
      {
         GrammarBuilder = grammarBuilder;
      }

      protected GrammarBuilder<T> GrammarBuilder { get; private set; }

      public YGrammar End
      {
         get
         {
            return new YGrammar( GrammarBuilder.EndGrammar() );
         }
      }

      public ProduceInterfaceWithNameAndTokActionAttribute<T> Token(T tokenType)
      {
         GrammarBuilder.AddToken(tokenType);
         return new ProduceInterfaceWithNameAndTokActionAttribute<T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndActionAttribute<T> Label(string label)
      {
         GrammarBuilder.AddLabel(label);
         return new ProduceInterfaceWithNameAndActionAttribute<T>(GrammarBuilder);
      }

      public ProduceInterFaceWithoutNameAndActionAttribute<T> Lambda(Action action)
      {
         GrammarBuilder.AddLambda(action);
         return new ProduceInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndActionAttribute<T> Gosub(Transition subGrammar)
      {
         GrammarBuilder.AddSubGrammar(subGrammar);
         return new ProduceInterfaceWithNameAndActionAttribute<T>(GrammarBuilder);
      }

      public ProduceInterFaceWithoutNameAndActionAttribute<T> Goto(string label)
      {
         GrammarBuilder.GotoLabel(label);
         return new ProduceInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }

      public ProduceInterFaceWithoutNameAndActionAttribute<T> Switch(params BranchBuilder<T>[] branches)
      {
         GrammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return new ProduceInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
