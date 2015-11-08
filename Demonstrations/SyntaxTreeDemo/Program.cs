using SyntaxTree;
using System;

namespace SyntaxTreeDemo
{
   class Program
   {
      static void Main(string[] args)
      {
         var treeGenerator
             = SyntaxTreeGenerator<TokenType>.Make
                                             .Register(new TokenDefinition())
                                             .Register(new GrammarDefinition())
                                             .Create();

         bool finished = false;
         while (!finished)
         {
            // Get input text
            Console.Write("Insert Expression or quit: ");
            var inputText = Console.ReadLine();
            Console.WriteLine();
            if (inputText != "quit")
            {
               var result = treeGenerator.Parse(inputText);
               if (result.Success)
               {

                  var node = result.Success ? result.Root : null;
                  var function = ExpressionTreeCreator.CreateExpression(node);
                  Console.WriteLine("Result: {0}", function());
               }
               else
               {
                  var position = result.ParserResult.Position;
                  Console.WriteLine("Syntax error: {0}", inputText);
                  var cursorText = "              " + new string(' ', position) + "#";
                  Console.WriteLine(cursorText);
               }
               Console.WriteLine();
            }
            else
            {
               finished = true;
            }
         }

      }
   }
}
