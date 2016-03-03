/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>
namespace Yallocc
{
   public class BranchInterfaceWithNameAndWithoutAction<TCtx, T> : BranchInterface<TCtx, T> where T : struct
   {
      public BranchInterfaceWithNameAndWithoutAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public BranchInterface<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new BranchInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
