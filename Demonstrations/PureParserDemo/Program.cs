using LexSharp;
using System;
using System.Collections.Generic;
using Yallocc;

namespace PureParserDemo
{
   class Program
   {
      static void Main(string[] args)
      {
         // Create tokenizer
         var lex = new LexSharp<TokenType>();
         lex.DefineTokens();

         // Create grammarbuilder and grammar
         var grammers = new GrammarDictionary();
         var baseBuilder = new GrammarBuilder<TokenType>(grammers);
         var builderInterface = new BuilderInterface<TokenType>(baseBuilder);
         builderInterface.DefineGrammar();
         var grammar = grammers.GetMasterGrammar();

         // Create parser
         var parser = new Parser<TokenType>();

         // Parse inputs from the console
         bool finished = false;
         while (!finished)
         {
            // Get input text
            Console.Write("Insert Expression or quit: ");
            var inputText = Console.ReadLine();

            // Scan text and parse resulting token sequence with created grammar.
            IEnumerable<Token<TokenType>> sequence = lex.Scan(inputText);
            ParserResult result = parser.ParseTokens(grammar, sequence);

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
