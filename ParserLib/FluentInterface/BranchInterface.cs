using System.Linq;

namespace ParserLib
{
   public class BranchInterface<T> : BranchBuilder<T>
   {
      protected BranchInterface(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterfaceWithNameAndTokActionAttribute<T> Token(T tokenType)
      {
         GrammarBuilder.AddToken(tokenType);
         return new BranchInterfaceWithNameAndTokActionAttribute<T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndActionAttribute<T> Label(string label)
      {
         GrammarBuilder.AddLabel(label);
         return new BranchInterfaceWithNameAndActionAttribute<T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndActionAttribute<T> Gosub(Transition subGrammar)
      {
         GrammarBuilder.AddSubGrammar(subGrammar);
         return new BranchInterfaceWithNameAndActionAttribute<T>(GrammarBuilder);
      }

      public BranchInterFaceWithoutNameAndActionAttribute<T> Goto(string label)
      {
         GrammarBuilder.GotoLabel(label);
         return new BranchInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndActionAttribute<T> Switch(params BranchBuilder<T>[] branches)
      {
         GrammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return new BranchInterfaceWithNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
