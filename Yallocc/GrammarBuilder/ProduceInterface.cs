using System;
using System.Linq;

namespace Yallocc
{
   public class ProduceInterface<T> where T : struct
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

      public ProduceInterfaceWithNameAndWithTokAction<T> Token(T tokenType)
      {
         GrammarBuilder.AddToken(tokenType);
         return new ProduceInterfaceWithNameAndWithTokAction<T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndWithAction<T> Label(string label)
      {
         GrammarBuilder.AddLabel(label);
         return new ProduceInterfaceWithNameAndWithAction<T>(GrammarBuilder);
      }

      public ProduceInterfaceWithNameAndWithAction<T> Lambda
      {
         get
         {
            GrammarBuilder.AddLambda();
            return new ProduceInterfaceWithNameAndWithAction<T>(GrammarBuilder);
         }
      }

      public ProduceInterfaceWithNameAndWithAction<T> Gosub(string nameOfSubGrammar)
      {
         GrammarBuilder.AddSubGrammar(nameOfSubGrammar);
         return new ProduceInterfaceWithNameAndWithAction<T>(GrammarBuilder);
      }

      public ProduceInterFaceWithoutNameAndWithoutAction<T> Goto(string label)
      {
         GrammarBuilder.GotoLabel(label);
         return new ProduceInterFaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }

      public ProduceInterFaceWithoutNameAndWithoutAction<T> Switch(params BranchBuilder<T>[] branches)
      {
         GrammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return new ProduceInterFaceWithoutNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
