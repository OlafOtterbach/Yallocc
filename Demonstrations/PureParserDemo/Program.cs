using System;
using System.Collections.Generic;
using Yallocc;
using Yallocc.Tokenizer;
using Yallocc.Tokenizer.LeTok;

namespace RawParserDemo
{
   class Program
   {
      static void Main(string[] args)
      {
         // Create tokenizer
         var tokenizerCreator = new LeTokCreator<TokenType>();
         tokenizerCreator.Register(@"\+", TokenType.plus);
         tokenizerCreator.Register(@"\-", TokenType.minus);
         tokenizerCreator.Register(@"\*", TokenType.mult);
         tokenizerCreator.Register(@"\/", TokenType.div);
         tokenizerCreator.Register(@"\(", TokenType.open);
         tokenizerCreator.Register(@"\)", TokenType.close);
         tokenizerCreator.Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         tokenizerCreator.Register(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         tokenizerCreator.RegisterIgnorePattern(@"( |\t)+", TokenType.white_space);
         var tokenizer = tokenizerCreator.Create();

         // Create grammarbuilder and grammar
         var grammers = new GrammarDictionary();
         var baseBuilder = new GrammarBuilder<DummyContext, TokenType>(grammers);
         var builderInterface = new GrammarBuilderInterface<DummyContext, TokenType>(baseBuilder);
         builderInterface.DefineGrammar();
         var grammar = grammers.GetMasterGrammar();

         // Create parser
         var parser = new Parser<DummyContext, TokenType>(grammar);

         // Parse inputs from the console
         bool finished = false;
         while (!finished)
         {
            // Get input text
            Console.Write("Insert Expression or quit: ");
            var inputText = Console.ReadLine();

            // Scan text and parse resulting token sequence with created grammar.
            IEnumerable<Token<TokenType>> sequence = tokenizer.Scan(inputText);
            ParserResult<TokenType> result = parser.ParseTokens(sequence, new DummyContext());

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
