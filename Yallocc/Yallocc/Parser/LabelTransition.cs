namespace Yallocc
{
   public class LabelTransition<TCtx> : ActionTransition<TCtx>
   {
      public LabelTransition(string name) : base()
      {
         Name = name;
      }
   }
}
