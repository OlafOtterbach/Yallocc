using Basic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicDemo
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private Canvas2DWpf _canvas;

      public MainWindow()
      {
         InitializeComponent();
         //InitCanvas();
      }

      private void InitCanvas()
      {
         var parent = Application.Current.MainWindow;
         _canvas = new Canvas2DWpf(this.BasicCanvas);
      }

      private void OnKeyDown(object sender, RoutedEventArgs e)
      {
      }


      private void OnResize(object t_sender, SizeChangedEventArgs t_event)
      {
         if (_canvas != null)
         {
            _canvas.Resize();
            for (int i = 10; i < 160; i++)
               _canvas.Plot(i, i, 1);
         }
      }
   }
}
