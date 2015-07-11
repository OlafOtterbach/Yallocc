using LexSharp;
using SyntaxTree;

namespace BasicDemo.Basic
{
   public class TokenTreeNode : SyntaxTreeNode
   {
      public TokenTreeNode(Token<TokenType> token)
      {
         Token = token;
      }

      public Token<TokenType> Token { get; private set; }
   }
}
