using System;

namespace Yallocc
{
   public class ExitInterfaceWithNameAndWithAction<T> : ExitInterface<T> where T : struct
   {
      public ExitInterfaceWithNameAndWithAction(GrammarBuilder<T> grammarBuilder) : base(grammarBuilder)
      {}

      public ExitInterfaceWithoutNameAndWithAction<T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ExitInterfaceWithoutNameAndWithAction<T>(GrammarBuilder);
      }

      public ExitInterfaceWithNameAndWithoutAction<T> Action(Action action)
      {
         GrammarBuilder.AddAction(action);
         return new ExitInterfaceWithNameAndWithoutAction<T>(GrammarBuilder);
      }
   }
}
