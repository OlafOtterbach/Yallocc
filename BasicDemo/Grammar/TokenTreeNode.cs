using LexSharp;
using SyntaxTree;

namespace BasicDemo.Grammar
{
   public class TokenTreeNode<T> : SyntaxTreeNode where T : struct
   {
      public TokenTreeNode(Token<T> token)
      {
         Token = token;
      }

      public Token<T> Token { get; private set; }
   }
}
