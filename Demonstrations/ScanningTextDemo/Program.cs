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
      private static void DefineTokens(LexSharp<long> lex)
      {
         lex.Register(@"000", 0);
         lex.Register(@"001", 1);
         lex.Register(@"010", 2);
         lex.Register(@"011", 3);
         lex.Register(@"100", 4);
         lex.Register(@"101", 5);
         lex.Register(@"110", 6);
         lex.Register(@"111", 7);
      }


      static void Main(string[] args)
      {
         const int limit = 10000;
         var rand = new Random();
         var binaries = Enumerable.Range(0, limit)
                                 .Select(i => rand.Next(0, 2))
                                 .Select(x => x.ToString())
                                 .Aggregate((current, elem) => current = current + elem);
         var lex = new LexSharp<long>();
         DefineTokens(lex);

binaries = "101111110110010000110001111110";
         var sequence = lex.Scan(binaries).ToList();
         Console.WriteLine("Text length: {0}", binaries.Length);
         Console.WriteLine("Sequence length: {0}", sequence.Count);
         var count0 = sequence.Where(x => x.Type == 0).Count();
         var count1 = sequence.Where(x => x.Type == 1).Count();
         var count2 = sequence.Where(x => x.Type == 2).Count();
         var count3 = sequence.Where(x => x.Type == 3).Count();
         var count4 = sequence.Where(x => x.Type == 4).Count();
         var count5 = sequence.Where(x => x.Type == 5).Count();
         var count6 = sequence.Where(x => x.Type == 6).Count();
         var count7 = sequence.Where(x => x.Type == 7).Count();
         var count8 = sequence.Where(x => x.Type == null).Count();
         var count = count0 + count1 + count2 + count3 + count4 + count5 + count6 + count7 + count8;
         Console.WriteLine(" 000 X {0}", count0);
         Console.WriteLine(" 001 X {0}", count1);
         Console.WriteLine(" 010 X {0}", count2);
         Console.WriteLine(" 011 X {0}", count3);
         Console.WriteLine(" 100 X {0}", count4);
         Console.WriteLine(" 101 X {0}", count5);
         Console.WriteLine(" 110 X {0}", count6);
         Console.WriteLine(" 111 X {0}", count7);
         Console.WriteLine(" ??? X {0}", count8);
         Console.WriteLine("---------------------");
         Console.WriteLine("Sum = {0}", count);
         Console.WriteLine();
         Console.WriteLine("??? = \"{0}\"", sequence.Where(x => x.Type == null).First().Value);
         var riddle = sequence.Where(x => x.Type == null).First();
         Console.WriteLine("??? = \"{0}\", Position = {1}", riddle.Type, riddle.TextIndex);
         Console.WriteLine("{0}...", binaries.Substring(0, Math.Min(40, limit)));
         Console.WriteLine(sequence[0].Value);
         Console.WriteLine(sequence[1].Value);
         Console.WriteLine(sequence[2].Value);
         Console.WriteLine(sequence[3].Value);
         Console.WriteLine(sequence[4].Value);
         Console.WriteLine(sequence[5].Value);
         Console.WriteLine(sequence[6].Value);
         Console.WriteLine(sequence[7].Value);
         Console.WriteLine(sequence[8].Value);
         Console.WriteLine("...");
         Console.ReadKey();
      }
   }
}
