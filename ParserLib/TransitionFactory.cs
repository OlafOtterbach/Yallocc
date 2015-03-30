namespace ParserLib
{
   internal class TransitionFactory<T>
   {
      public TransitionFactory()
      {}

      Transition CreateTokenTransition( T tokenType )
      {
         return new SyntaxNodeTransition<T>(new SyntaxNode<T>(tokenType));
      }
   }
}
