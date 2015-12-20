using System;
using System.Linq;

namespace ScanningBasicDemo
{
   class Program
   {
      static string CreateBasicText(int limit)
      {
         var words = new System.Collections.Generic.List<string>
         {
            "PROGRAM",
            "END",
            "+",
            "-",
            "*",
            "/",
            "=",
            ">",
            ">=",
            "<",
            "<=",
            "(",
            ")",
            ",",
            ":",
            "\r\n",
            "1234",
            "123.456",
            "Text",
            "DIM",
            "LET",
            "IF",
            "THEN",
            "ELSE",
            "WHILE",
            "FOR",
            "TO",
            "STEP",
            "DO",
            "GOTO",
            "PLOT",
            "NOT",
            "AND",
            "OR",
            "MOD",
            "Name"
         };

         var rand = new Random();
         var text = Enumerable.Range(0, limit)
                              .Select(i => words[rand.Next(0,words.Count)])
                              .Aggregate((current, elem) => current = current + " " + elem);
         return text;
      }

      static void Main(string[] args)
      {
         const int limit = 100000;
         var lex = TokenDefinition.CreateTokenizer();
         Console.WriteLine("Generating text with {0} characters...", limit);
         var basicTest = CreateBasicText(limit);

         Console.WriteLine("Scanning text with {0} characters...", limit);
         DateTime startTime = DateTime.Now;

         var sequence = lex.Scan(basicTest).ToList();

         DateTime endTime = DateTime.Now;
         TimeSpan deltaTime = endTime - startTime;
         Console.WriteLine("Time: {0} ms", deltaTime.Milliseconds);
         var count = sequence.Where(x => x.Type == null).Count();
         Console.WriteLine("Count of Unknown words: {0}", count);
         if(count == 0)
         {
            Console.WriteLine("All words are identified");
         }
         Console.WriteLine();
         for(int i = 0; i < 10; i++)
         {
            Console.WriteLine("{0} : {1}", i, sequence[i].Value);
         }
         Console.WriteLine("...");
         Console.ReadKey();
      }
   }
}

