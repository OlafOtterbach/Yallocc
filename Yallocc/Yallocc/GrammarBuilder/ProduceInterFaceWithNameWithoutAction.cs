namespace Yallocc
{
   public class ProduceInterfaceWithNameAndWithoutAction<TCtx,T> : ProduceInterface<TCtx, T> where T : struct
   {
      public ProduceInterfaceWithNameAndWithoutAction(GrammarBuilder<TCtx, T> grammarBuilder) : base(grammarBuilder)
      {}

      public ProduceInterface<TCtx, T> Name(string name)
      {
         GrammarBuilder.AddName(name);
         return new ProduceInterface<TCtx, T>(GrammarBuilder);
      }
   }
}
