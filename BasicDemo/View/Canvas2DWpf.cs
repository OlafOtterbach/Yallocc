using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Basic.View
{
   /// <summary>
   /// WPF bitmap canvas to draw 2D geometry.
   /// </summary>
   public class Canvas2DWpf : Canvas2D
   {
      Canvas _canvas;

      private Image _image;

      private WriteableBitmap _bitmap;

      public Canvas2DWpf(Canvas canvas) : base()
      {
         Resize();
         _canvas = canvas;
         _image = new Image();
         canvas.Children.Add(_image);
      }

      public override void Resize()
      {
         int width = 320;
         int height = 200;
         if( ( width > 0 ) && ( height > 0 ) )
         {
            _bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr32, null);
            _image.Source = _bitmap;
            Resize(width, height, _bitmap.BackBufferStride);
         }
      }

      public override void Refresh()
      {
         if( _bitmap != null )
         {
            _bitmap.WritePixels(new Int32Rect(0, 0, GetWidth(), GetHeight()), Buffer, _bitmap.BackBufferStride, 0);
         }
      }
   }
}
