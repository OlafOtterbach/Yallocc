using Yallocc.SyntaxTree;
using System.Linq;

namespace BasicDemo.Basic
{
   public class LabelCommandCreator : CommandCreator
   {
      public LabelCommandCreator(BasicEngine engine) : base(engine)
      { }

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode<TokenType>) && ((node as TokenTreeNode<TokenType>).Token.Type == TokenType.label);
         return result;
      }

      public override void Create(SyntaxTreeNode node)
      {
         var tokNode = node as TokenTreeNode<TokenType>;
         var name = tokNode.Token.Value;
         var labelName = name.Length > 0 ? name.Substring(0, name.Length - 1 ) : string.Empty;
         var command = new LabelCommand(tokNode.Token, Engine, labelName);
         Engine.Add(command);
      }
   }
}
