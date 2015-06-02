﻿namespace Yallocc
{
   public class BeginInterface<T> where T : struct
   {
      private GrammarBuilder<T> _grammarBuilder;

      public BeginInterface(GrammarBuilder<T> grammarBuilder)
      {
         _grammarBuilder = grammarBuilder;
      }

      public ProduceInterFaceWithoutNameAndActionAttribute<T> Begin
      {
         get
         {
            _grammarBuilder.BeginGrammar();
            return new ProduceInterFaceWithoutNameAndActionAttribute<T>(_grammarBuilder);
         }
      }
   }
}
