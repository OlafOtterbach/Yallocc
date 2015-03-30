namespace ParserLib
{
   internal class GrammarTransition : Transition
   {
      public GrammarTransition( Grammar grammar)
      {
         Grammar = grammar;
      }

      public Grammar Grammar { get; private set; }
   }
}
