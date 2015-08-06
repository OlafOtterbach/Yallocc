using SyntaxTree;

namespace BasicDemo.Basic
{
   public class LetCommandCreator : CommandCreator
   {
      public LetCommandCreator(BasicEngine engine) : base(engine)
      {}

      public override BasicCommand Create(SyntaxTreeNode node)
      {
         return null;
      }
   }
}
