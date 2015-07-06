namespace SyntaxTreeTest.ExpressionTree
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

      public bool IsDouble { get; private set; }

      public double DoubleValue { get; private set; }

      public bool IsBoolean { get; private set; }

      public bool BooleanValue { get; private set; }
   }
}
