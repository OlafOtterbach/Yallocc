namespace Yallocc
{
   public struct YGrammar
   {
      public YGrammar(Transition startOfGrammar) : this()
      {
         StartOfGrammar = startOfGrammar;
      }

      public Transition StartOfGrammar { get; private set; }
   }
}
