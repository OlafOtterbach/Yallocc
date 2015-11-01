using LexSharp;

namespace BasicDemo.Basic
{
   public class PlotCommand : BasicCommand
   {
      private ExpressionCommand _xExpression;
      private ExpressionCommand _yExpression;
      private ExpressionCommand _colorExpression;

      public PlotCommand(Token<TokenType> startToken, BasicEngine engine, ExpressionCommand xExpression, ExpressionCommand yExpression, ExpressionCommand colorExpression) : base(startToken, engine)
      {
         _xExpression = xExpression;
         _yExpression = yExpression;
         _colorExpression = colorExpression;
      }

      public override void Execute()
      {
         if (Engine.Canvas != null)
         {
            var xVar = _xExpression.Execute();
            if (!(xVar is BasicInteger))
            {
               throw new BasicTypeMissmatchException("Plot expression for x value is not of integer type.");
            }
            var yVar = _yExpression.Execute();
            if (!(yVar is BasicInteger))
            {
               throw new BasicTypeMissmatchException("Plot expression for y value is not of integer type.");
            }
            var colorVar = _colorExpression.Execute();
            if(colorVar is BasicArrayElementAccessor)
            {
               colorVar = (colorVar as BasicArrayElementAccessor).Value;
            }
            if (!(colorVar is BasicInteger))
            {
               throw new BasicTypeMissmatchException("Plot expression for color value is not of integer type.");
            }
            var x = (xVar as BasicInteger).Value;
            var y = (yVar as BasicInteger).Value;
            var color = (colorVar as BasicInteger).Value;
            Engine.Canvas.Plot(x, y, color);
            Engine.Cursor.Next();
         }
      }
   }
}
