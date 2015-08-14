using SyntaxTree;

namespace BasicDemo.Basic
{
   public abstract class CommandCreator
   {
      public CommandCreator(BasicEngine engine)
      {
         Engine = engine;
      }

      public BasicEngine Engine { get; set; }

      public abstract bool CanCreate(SyntaxTreeNode node);

      public abstract BasicCommand Create(SyntaxTreeNode node);
   }
}
