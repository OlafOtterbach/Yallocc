using System;
using System.Linq;

namespace Yallocc
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

      public BranchInterFaceWithoutNameAndActionAttribute<T> Lambda(Action action)
      {
         GrammarBuilder.AddLambda(action);
         return new BranchInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }

      public BranchInterFaceWithoutNameAndActionAttribute<T> Default
      {
         get
         {
            return new BranchInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
         }
      }

      public BranchInterfaceWithNameAndActionAttribute<T> Gosub(YGrammar subGrammar)
      {
         GrammarBuilder.AddSubGrammar(subGrammar);
         return new BranchInterfaceWithNameAndActionAttribute<T>(GrammarBuilder);
      }

      public BranchInterFaceWithoutNameAndActionAttribute<T> Goto(string label)
      {
         GrammarBuilder.GotoLabel(label);
         return new BranchInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }

      public BranchInterFaceWithoutNameAndActionAttribute<T> Switch(params BranchBuilder<T>[] branches)
      {
         GrammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return new BranchInterFaceWithoutNameAndActionAttribute<T>(GrammarBuilder);
      }
   }
}
