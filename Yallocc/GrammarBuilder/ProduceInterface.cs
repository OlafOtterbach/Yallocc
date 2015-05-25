using System;
using System.Linq;

namespace Yallocc
{
   public class ProduceInterface<T>
   {
      protected ProduceInterface(GrammarBuilder<T> grammarBuilder)
      {
         GrammarBuilder = grammarBuilder;
      }

      protected GrammarBuilder<T> GrammarBuilder { get; private set; }

      public void End()
      {
         GrammarBuilder.EndGrammar();
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

      public ProduceInterFaceWithoutActionAttribute<T> Default
      {
         get
         {
            GrammarBuilder.AddLambda();
            return new ProduceInterFaceWithoutActionAttribute<T>(GrammarBuilder);
         }
      }

      public ProduceInterfaceWithNameAndActionAttribute<T> Gosub(string nameOfSubGrammar)
      {
         GrammarBuilder.AddSubGrammar(nameOfSubGrammar);
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
