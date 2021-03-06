﻿/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>
namespace Yallocc
{
   public class ExitInterfaceWithNameAndWithoutAction<TCtx, T> : ExitInterface<TCtx, T> where T : struct
   {
      public ExitInterfaceWithNameAndWithoutAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public ExitInterface<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ExitInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
