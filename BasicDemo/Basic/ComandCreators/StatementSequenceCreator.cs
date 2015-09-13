using SyntaxTree;
using System.Collections.Generic;
using System.Linq;

namespace BasicDemo.Basic
{
   public class StatementSequenceCreator
   {
      private List<CommandCreator> _creators;
      private BasicEngine _engine;

      public StatementSequenceCreator(BasicEngine engine)
      {
         _engine = engine;
         _creators = new List<CommandCreator>
         {
            new LetCommandCreator(engine),
            new DimCommandCreator(engine),
            new GotoCommandCreator(engine),
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
               var command = canCreator.Create(node);
               _engine.Add(command);
            }
         }
      }
   }
}