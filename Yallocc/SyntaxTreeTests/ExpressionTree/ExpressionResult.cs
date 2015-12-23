namespace Yallocc.SyntaxTreeTest.ExpressionTree
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
         BooleanValue = val;
         IsBoolean = true;
      }

      public bool IsDouble { get; }

      public double DoubleValue { get; }

      public bool IsBoolean { get; }

      public bool BooleanValue { get; }
   }
}
