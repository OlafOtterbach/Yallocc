using LexSharp;
using System;
using System.Collections.Generic;

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
         var lex = new LexSharp<TokenType>();

         // Define tokens
         lex.Register(@"\+", TokenType.plus);
         lex.Register(@"\-", TokenType.minus);
         lex.Register(@"\*", TokenType.mult);
         lex.Register(@"\/", TokenType.div);
         lex.Register(@"\(", TokenType.open);
         lex.Register(@"\)", TokenType.close);
         lex.Register(@"(0|1|2|3|4|5|6|7|8|9)+", TokenType.integer);
         lex.Register(@"(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+", TokenType.real);
         lex.Register(@"( |\t)+", TokenType.white_space);

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
                  Console.WriteLine("Integer: {0}, Position {1}", token.Value, token.TextIndex);
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
