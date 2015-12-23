using System;
using System.Linq;
using Yallocc.Tokenizer;
using Yallocc.Tokenizer.LeTok;

namespace ScanningBitGroupsDemo
{
   class Program
   {
      private static Tokenizer<long> CreateTokenizer()
      {
         var tokenizerCreator = new LeTokCreator<long>();
         tokenizerCreator.Register(@"000", 0);
         tokenizerCreator.Register(@"001", 1);
         tokenizerCreator.Register(@"010", 2);
         tokenizerCreator.Register(@"011", 3);
         tokenizerCreator.Register(@"100", 4);
         tokenizerCreator.Register(@"101", 5);
         tokenizerCreator.Register(@"110", 6);
         tokenizerCreator.Register(@"111", 7);
         var tokenizer = tokenizerCreator.Create();
         return tokenizer;
      }


      static void Main(string[] args)
      {
         const int elementsLimit = 10000;
         const int limit = elementsLimit * 3;
         var rand = new Random();
         var binaries = Enumerable.Range(0, limit)
                                 .Select(i => rand.Next(0, 2))
                                 .Select(x => x.ToString())
                                 .Aggregate((current, elem) => current = current + elem);
         var tokenizer = CreateTokenizer();

         Console.WriteLine("Sacanning Text with {0} characters...", limit);
         DateTime startTime = DateTime.Now;
         var sequence = tokenizer.Scan(binaries).ToList();
         DateTime endTime = DateTime.Now;
         TimeSpan deltaTime = endTime - startTime;
         Console.WriteLine("Time: {0} ms", deltaTime.Milliseconds);

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
         if (sequence.Any(x => x.Type == null))
         {
            var riddle = sequence.Where(x => x.Type == null).First();
            Console.WriteLine("??? = \"{0}\", Position = {1}", riddle.Value, riddle.TextIndex);
         }
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
