using System.Linq;

namespace ParserLib
{
   public class ProducerInterface<T>
   {
      protected ProducerInterface(GrammarBuilder<T> grammarBuilder)
      {
         GrammarBuilder = grammarBuilder;
      }

      protected GrammarBuilder<T> GrammarBuilder { get; private set; }

      public Transition End()
      {
         return GrammarBuilder.EndGrammar();
      }

      public ProducerInterfaceWithNameAndActionAttribute<T> Token(T tokenType)
      {
         GrammarBuilder.AddToken(tokenType);
         return new ProducerInterfaceWithNameAndActionAttribute<T>(GrammarBuilder);
      }

      public ProducerInterfaceWithNameAndActionAttribute<T> Label(string label)
      {
         GrammarBuilder.AddLabel(label);
         return new ProducerInterfaceWithNameAndActionAttribute<T>(GrammarBuilder);
      }

      public ProducerInterfaceWithNameAndActionAttribute<T> Gosub(Transition subGrammar)
      {
         GrammarBuilder.AddSubGrammar(subGrammar);
         return new ProducerInterfaceWithNameAndActionAttribute<T>(GrammarBuilder);
      }

      public ProducerInterfaceWithNameAndActionAttribute<T> Switch(params BranchBuilder<T>[] branches)
      {
         GrammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return new ProducerInterfaceWithNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
