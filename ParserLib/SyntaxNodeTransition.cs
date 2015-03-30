namespace ParserLib
{
   internal class SyntaxNodeTransition<T> : Transition
   {
      public SyntaxNodeTransition( SyntaxNode<T> syntaxNode )
      {
         Node = syntaxNode;
      }

      public SyntaxNode<T> Node { get; set; }
   }
}
