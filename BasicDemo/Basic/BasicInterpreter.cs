using Basic.View;
using SyntaxTree;

namespace BasicDemo.Basic
{
   public class BasicInterpreter
   {
      private string _programText;
      private BasicEngine _engine;

      public BasicInterpreter(Canvas2D canvas)
      {
         _programText = string.Empty;
         _engine = new BasicEngine();
         _engine.Canvas = canvas;
      }

      public void Run()
      {
         if(_programText != string.Empty)
         {
            CreateBasicRuntimeCode(_programText);
            _engine.Run();
         }
      }

      public void Load(string programText)
      {
         _programText = programText;
      }

      private void CreateBasicRuntimeCode(string programText)
      {
         var root = CreateSyntaxTree(programText);
         var programCreator = new ProgramCreator(_engine);
         programCreator.Create(root);
      }

      private SyntaxTreeNode CreateSyntaxTree(string programText)
      {
         var grammarGenerator = new BasicGrammarGenerator();
         var res = grammarGenerator.Parse(programText);
         return res.Root;
      }
   }
}
