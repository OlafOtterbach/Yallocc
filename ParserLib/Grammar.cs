namespace ParserLib
{
   public class Grammar
   {
      public Grammar(Transition startTransition)
      {
         StartTransition = startTransition;
      }

      public Transition StartTransition { get; private set; }
   }
}
