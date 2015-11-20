using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LexSharp
{
   [TestClass]
   public class LeTokTest
   {
      [TestMethod]
      public void Test()
      {
         var lex = Create();
         var binaries = "101Hugo111110110010000110001111110";

         var sequence = lex.Scan(binaries).ToList();
      }

      private LeTok<long> Create()
      {
         var lex = new LeTok<long>();
         lex.Register(@"000", 0);
         lex.Register(@"001", 1);
         lex.Register(@"010", 2);
         lex.Register(@"011", 3);
         lex.Register(@"100", 4);
         lex.Register(@"101", 5);
         lex.Register(@"110", 6);
         lex.Register(@"111", 7);
         lex.Init();
         return lex;
      }
   }
}