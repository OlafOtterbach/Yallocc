using SyntaxTree;

namespace BasicDemo.Basic
{
   public class DimCommandCreator : CommandCreator
   {
      public DimCommandCreator(BasicEngine engine) : base(engine)
      {}

      public override BasicCommand Create(SyntaxTreeNode node)
      {
         return null;
      }
   }
}
