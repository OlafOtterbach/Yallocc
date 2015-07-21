using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxTree
{
   public class SyntaxTreeGeneratorNotReadyException : Exception
   {
      public SyntaxTreeGeneratorNotReadyException()
      {
      }

      public SyntaxTreeGeneratorNotReadyException(string message)
         : base(message)
      {
      }

      public SyntaxTreeGeneratorNotReadyException(string message, Exception inner)
         : base(message, inner)
      {
      }
   }
}
