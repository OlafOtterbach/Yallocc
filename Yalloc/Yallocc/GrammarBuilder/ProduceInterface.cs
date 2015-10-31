using System;
using System.Linq;

namespace Yallocc
{
   public class ProduceInterface<T> where T : struct
   {
      public ProduceInterface(GrammarBuilder<T> grammarBuilder)
      {
         GrammarBuilder = grammarBuilder;
      }

      protected GrammarBuilder<T> GrammarBuilder { get; private set; }

      public ExitInterfaceWithNameAndWithAction<T> Exit
      {
         get
         {
            GrammarBuilder.AddLambda();
            return new ExitInterfaceWithNameAndWithAction<T>(GrammarBuilder);
         }
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

      public GosubInterfaceWithoutNameAndWithAction<T> Gosub(string nameOfSubGrammar)
      {
         GrammarBuilder.AddSubGrammar(nameOfSubGrammar);
         return new GosubInterfaceWithoutNameAndWithAction<T>(GrammarBuilder);
      }

      public ProduceInterface<T> Goto(string label)
      {
         GrammarBuilder.GotoLabel(label);
         return new ProduceInterface<T>(GrammarBuilder);
      }

      public ProduceInterface<T> Switch(params BranchBuilder<T>[] branches)
      {
         GrammarBuilder.Switch(branches.Select(x => x.GrammarBuilder).ToArray());
         return new ProduceInterface<T>(GrammarBuilder);
      }
   }
}
