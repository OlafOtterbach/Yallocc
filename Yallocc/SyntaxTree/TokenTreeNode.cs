using Yallocc.Tokenizer;

namespace Yallocc.SyntaxTree
{
   public class TokenTreeNode<T> : SyntaxTreeNode where T : struct
   {
      public TokenTreeNode(Token<T> token)
      {
         Token = token;
      }

      public Token<T> Token { get; }
   }
}
