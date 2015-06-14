﻿namespace Yallocc
{
   public class BranchBuilder<T> where T : struct
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BranchBuilder(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public GrammarBuilder<T> GrammarBuilder
      {
         get
         {
            return _grammarBuilder;
         }
      }
   }
}
