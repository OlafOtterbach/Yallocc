using LexSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScanningTextDemo
{
   class Program
   {
      public enum TokenType
      {
         comma,           // ,
         dot,             // .
         minus,           // -
         colon,           // :
         semicolon,       // ;
         tag,             // <tag>
         word             // word
      }

      private static void DefineTokens(LexSharp<TokenType> lex)
      {
         lex.Register(@",", TokenType.comma);
         lex.Register(@"\.", TokenType.dot);
         lex.Register(@"-", TokenType.minus);
         lex.Register(@":", TokenType.colon);
         lex.Register(@";", TokenType.semicolon);
         lex.Register(@"\<(.)*\>", TokenType.tag);
         lex.Register(@"(\w)+(\w|\d)*", TokenType.word);
      }

      private struct Counter
      {
         public Counter(TokenType? typ)
         {
            Comma = 0;
            Dot = 0;
            Minus = 0;
            Colon = 0;
            Semicolon = 0;
            Tag = 0;
            Word = 0;
            switch(typ)
            {
               case TokenType.comma:
                  Comma = 1;
                  break;
               case TokenType.dot:
                  Dot = 1;
                  break;
               case TokenType.colon:
                  Colon = 1;
                  break;
               case TokenType.semicolon:
                  Semicolon = 1;
                  break;
               case TokenType.tag:
                  Tag = 1;
                  break;
               case TokenType.word:
                  Word = 1;
                  break;
            }
         }

         public int Comma;
         public int Dot;
         public int Minus;
         public int Colon;
         public int Semicolon;
         public int Tag;
         public int Word;
      }

      static void Main(string[] args)
      { 
         var webClient = new WebClient();
         var pageSourceCode = webClient.DownloadString(@"https://de.wikipedia.org/wiki/Faust._Eine_Trag%C3%B6die.");

         var lex = new LexSharp<TokenType>();
         DefineTokens(lex);

         var sequence = lex.Scan(pageSourceCode).ToList();
         var counts = sequence.Where(x => x.Type!= null).Select(x => new Counter(x.Type));
      }
   }
}
