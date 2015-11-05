using SyntaxTree;
using System.Collections.Generic;
using System.Linq;

namespace BasicDemo.Basic
{
   public class StatementSequenceCreator
   {
      private List<CommandCreator> _creators;

      public StatementSequenceCreator(BasicEngine engine)
      {
         _creators = new List<CommandCreator>
         {
            new IfCommandCreator(engine),
            new WhileCommandCreator(engine),
            new ForCommandCreator(engine),
            new LetCommandCreator(engine),
            new DimCommandCreator(engine),
            new GotoCommandCreator(engine),
            new PlotCommandCreator(engine),
            new LabelCommandCreator(engine),
            new EndOfProgramCommandCreator(engine)
         };
      }

      public void Create(IEnumerable<SyntaxTreeNode> nodes)
      {
         foreach (var node in nodes)
         {
            var canCreators = _creators.Where(creator => creator.CanCreate(node));
            if(canCreators.Any())
            {
               var canCreator = canCreators.First();
               canCreator.Create(node);
            }
         }
      }
   }
}