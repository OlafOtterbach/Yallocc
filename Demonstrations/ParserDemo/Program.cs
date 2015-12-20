using System;
using Yallocc;
using Yallocc.Tokenizer.LeTok;

namespace ParserDemo
{
   class Program
   {
      static void Main(string[] args)
      {
         // Define and create parser
         var tokenizerCreator = new LeTokCreator<TokenType>();
         var yalocc = new Yallocc<DummyContext, TokenType>(tokenizerCreator);
         yalocc.DefineParserTokens();
         yalocc.DefineParserGrammar();
         ParserAndTokenizer<DummyContext, TokenType> parser = yalocc.CreateParser();

         // Parse inputs from the console
         bool finished = false;
         while (!finished)
         {
            // Get input text
            Console.Write("Insert Expression or quit: ");
            var inputText = Console.ReadLine();

            ParserResult result = parser.Parse(inputText, new DummyContext());

            if (inputText != "quit")
            {
               Console.WriteLine();
               if (result.Success)
               {
                  Console.WriteLine("Expression is correct!");
               }
               else
               {
                  var position = result.Position;
                  Console.WriteLine("Syntax error: {0}",inputText);
                  var cursorText =  "              " + new string(' ', position) + "#";
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
