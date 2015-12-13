using Yallocc.Tokenizer;

namespace SyntaxTree
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
