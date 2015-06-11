using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YalloccSyntaxTreeTest
{
   public struct ExpressionResult
   {
      public ExpressionResult(double val) : this()
      {
         DoubleValue = val;
         IsDouble = true;
      }

      public ExpressionResult(bool val) :this()
      {
         BoolValue = val;
         IsBool = true;
      }

      public bool IsDouble { get; private set; }

      public double DoubleValue { get; private set; }

      public bool IsBool { get; private set; }

      public bool BoolValue { get; private set; }
   }
}
