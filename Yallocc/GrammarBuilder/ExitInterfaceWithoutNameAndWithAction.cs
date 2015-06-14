using System;

namespace Yallocc
{
   public class ExitInterfaceWithoutNameAndWithAction<T> : ExitInterface<T> where T : struct
   {
      public ExitInterfaceWithoutNameAndWithAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ExitInterface<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ExitInterface<T>(GrammarBuilder);
      }
   }
}
