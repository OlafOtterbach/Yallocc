using System;
using System.Linq;

namespace Yallocc
{
   public class BranchInterface<T> : BranchBuilder<T> where T : struct
   {
      protected BranchInterface(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterfaceWithNameAndWithTokAction<T> Token(T tokenType)
      {
         GrammarBuilder.AddToken(tokenType);
         return new BranchInterfaceWithNameAndWithTokAction<T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndWithAction<T> Label(string label)
      {
         GrammarBuilder.AddLabel(label);
         return new BranchInterfaceWithNameAndWithAction<T>(GrammarBuilder);
      }

      public BranchInterfaceWithNameAndWithAction<T> Lambda
      {
         get
         {
            GrammarBuilder.AddLambda();
            return new BranchInterfaceWithNameAndWithAction<T>(GrammarBuilder);
         }
      }

      public BranchDefaultInterfaceWithNameAndWithAction<T> Default
      {
         get
         {
            GrammarBuilder.AddLambda();
            return new BranchDefaultInterfaceWithNameAndWithAction<T>(GrammarBuilder);
         }
      }

      public BranchInterfaceWithNameAndWithAction<T> Gosub(string nameOfSubGrammar)
      {
         GrammarBuilder.AddSubGrammar(nameOfSubGrammar);
         return new BranchInterfaceWithNameAndWithAction<T>(GrammarBuilder);
      }

      public BranchInterFaceWithoutNameAndWithoutAction<T> Goto(string label)
      {
         GrammarBuilder.GotoLabel(label);
         return new BranchInterFaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }

      public BranchInterFaceWithoutNameAndWithoutAction<T> Switch(params BranchBuilder<T>[] branches)
      {
         GrammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return new BranchInterFaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
