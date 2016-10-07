using System;
using System.Collections.Generic;
using Yallocc.Tokenizer;
using Yallocc.Tokenizer.LexSharp;

namespace LexSharpDemo
{
   class Program
   {
      public enum TokenType
      {
         plus,            // +
         minus,           // -
         mult,            // *
         div,             // /
         open,            // (
         close,           // )
         integer,         // 1, 2, 3, 12, 123, ...
         real,            // 1.0, 12.0, 1.0, 0.2, .4, ...
         white_space      // _, TAB
      }

      static void Main(string[] args)
      {
         // Create tokenizer
         var creator = new LexSharpCreator<TokenType>();

         // Define tokens
         creator.Register(@"\+", TokenType.plus);
         creator.Register(@"\-", TokenType.minus);
         creator.Register(@"\*", TokenType.mult);
         creator.Register(@"\/", TokenType.div);
         creator.Register(@"\(", TokenType.open);
         creator.Register(@"\)", TokenType.close);
         creator.Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         creator.Register(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         creator.Register(@"( |\t)+", TokenType.white_space);
         var lex = creator.Create();

         // Get input text
         Console.Write("Insert Expression: ");
         var input = Console.ReadLine();
         Console.WriteLine();

         // Scan text
         IEnumerable<Token<TokenType>> tokens = lex.Scan(input);

         // Write recognised tokens
         foreach (Token<TokenType> token in tokens)
         {
            switch( token.Type )
            {
               case TokenType.integer:
                  Console.WriteLine("Integer: {0}, Position {1}", token.Value, token.TextIndex);
                  break;
               case TokenType.real:
                  Console.WriteLine("Real: {0}, Position {1}", token.Value, token.TextIndex);
                  break;
               case TokenType.plus:
                  Console.WriteLine("Addition: {0}, Position {1}", token.Value, token.TextIndex);
                  break;
               case TokenType.minus:
                  Console.WriteLine("Subtraction: {0}, Position {1}", token.Value, token.TextIndex);
                  break;
               case TokenType.mult:
                  Console.WriteLine("Multiplication: {0}, Position {1}", token.Value, token.TextIndex);
                  break;
               case TokenType.div:
                  Console.WriteLine("Division: {0}, Position {1}", token.Value, token.TextIndex);
                  break;
               case TokenType.open:
                  Console.WriteLine("Clamp open: {0}, Position {1}", token.Value, token.TextIndex);
                  break;
               case TokenType.close:
                  Console.WriteLine("Clamp close: {0}, Position {1}", token.Value, token.TextIndex);
                  break;
               default:
                  Console.WriteLine("Unknown: {0}, Position {1}", token.Value, token.TextIndex);
                  break;
            }
         }

         // End with pressing key
         Console.WriteLine();
         Console.WriteLine(input);
         Console.ReadKey();
      }
   }
}
