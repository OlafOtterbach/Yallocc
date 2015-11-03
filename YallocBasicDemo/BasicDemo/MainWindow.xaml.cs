using Basic.View;
using BasicDemo.Basic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

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
         InitCanvas();
      }

      private void InitCanvas()
      {
         var parent = Application.Current.MainWindow;
         _canvas = new Canvas2DWpf(BasicImage);
      }

      private async void OnKeyDown(object sender, RoutedEventArgs e)
      {
         BasicImage.Visibility = Visibility.Hidden;
         StartImage.Visibility = Visibility.Hidden;
         RunImage.Visibility = Visibility.Visible;
         _canvas.Resize();

         Task future = Task.Factory.StartNew
         ( () =>
         {
            var programText = File.ReadAllText(@"AppleMan.basic");
            var basic = new BasicInterpreter(_canvas);
            basic.Load(programText);
            basic.Run();
         });

         await future;
         BasicImage.Visibility = Visibility.Visible;
         StartImage.Visibility = Visibility.Hidden;
         RunImage.Visibility = Visibility.Hidden;
         _canvas.Refresh();
      }

      private string GetTextResourceFile(string resourceName)
      {
         var reso = this.Resources;
         var res = Assembly.GetExecutingAssembly().GetManifestResourceNames();
         var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
         using (var sr = new StreamReader(stream))
         {
            return sr.ReadToEnd();
         }
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
