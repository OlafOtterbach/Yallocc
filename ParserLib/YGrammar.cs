namespace ParserLib
{
   public struct YGrammar
   {
      internal YGrammar(Transition startOfGrammar)
      {
         StartOfGrammar = startOfGrammar;
      }

      internal Transition StartOfGrammar { get; private set; }
   }
}
