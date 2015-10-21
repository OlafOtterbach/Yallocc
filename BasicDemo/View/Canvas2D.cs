using System;

namespace Basic.View
{
   public abstract class Canvas2D
   {
      private const int _PixelSize = 4;

      private uint[] _buffer = null;

      private uint[] _clearBuffer = null;

      uint _penColor = 0;

      private int _width;

      private int _height;

      private int _alignWidth;

      public Canvas2D()
      {
         SetPen(0, 255, 0);
      }
      
      public double Width { get { return _width; } }

      public double Height { get { return _height; } }

      public abstract void Resize();

      public abstract void Refresh();

      public void SetPen( int red, int green, int blue )
      {
         uint ured = (uint)red;
         uint ugreen = (uint)green;
         uint ublue = (uint)blue;
         _penColor = ublue | (ugreen << 8) | (ured << 16) | (0xFF000000);
      }

      public void SetPen(uint color)
      {
         _penColor = color;
      }

      public void Plot(int x, int y, int color)
      {
         switch(color)
         {
            case 0:
               SetPen(0, 0, 0);
               break;
            case 1:
               SetPen(0, 0, 255);
               break;
            case 2:
               SetPen(0, 255, 0);
               break;
            default:
               SetPen(255, 0, 0);
               break;
         }

         var ywidth = _alignWidth / _PixelSize;
         var adresse = (_height - y - 1) * ywidth + x;
         if (adresse < _buffer.Length)
         {
            _buffer[adresse] = _penColor;
         }
      }

      public void Clear()
      {
         if (_buffer != null)
         {
            Array.Copy(_clearBuffer, _buffer, _buffer.Length);
         }
      }

      protected uint[] Buffer { get { return _buffer; } }

      protected void Resize(int width, int height, int alignWidth)
      {
         _width = width;
         _height = height;
         _alignWidth = alignWidth;
         int memsize = (alignWidth * height) / _PixelSize;
         _buffer = new uint[memsize];
         _clearBuffer = new uint[memsize];
         int clearSize = _clearBuffer.Length;
         for (int i = 0; i < clearSize; i++)
         {
            _clearBuffer[i] = 0x00000000;
         }
         Clear();
      }

      public int GetWidth()
      {
         return _width;
      }

      public int GetHeight()
      {
         return _height;
      }
   }
}
