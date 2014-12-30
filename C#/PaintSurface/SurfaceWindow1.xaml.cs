using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Threading.Tasks;
namespace PaintSurface
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        private Process _serverProcess;

        private int _serverPort = 8080;
        private bool brosseadentBool = false, verreBool = false, dentifriceBool = false;
        private SocketManager _sm;
        private MediaPlayer cuisine = new MediaPlayer();
        private MediaPlayer salon = new MediaPlayer();
        private MediaPlayer salledebain = new MediaPlayer();
        private MediaPlayer coiffez = new MediaPlayer();
        private MediaPlayer rasez = new MediaPlayer();
        private MediaPlayer brossezdent = new MediaPlayer();
        private MediaPlayer douchez = new MediaPlayer();
        private MediaPlayer brosseadentSon = new MediaPlayer();
        private MediaPlayer dentifriceSon = new MediaPlayer();
        private MediaPlayer verreSon = new MediaPlayer();
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            //les sons
            cuisine.Open(new Uri(@"Resources\cuisine.wav", UriKind.Relative));
            salon.Open(new Uri(@"Resources\salon.wav", UriKind.Relative));
            salledebain.Open(new Uri(@"Resources\salledebain.wav", UriKind.Relative));
            coiffez.Open(new Uri(@"Resources\coiffez.wav", UriKind.Relative));
            rasez.Open(new Uri(@"Resources\rasez.wav", UriKind.Relative));
            douchez.Open(new Uri(@"Resources\douchez.wav", UriKind.Relative));
            brossezdent.Open(new Uri(@"Resources\brossezlesdents.wav", UriKind.Relative));
            brosseadentSon.Open(new Uri(@"Resources\sonBrosseDent.wav", UriKind.Relative));
            dentifriceSon.Open(new Uri(@"Resources\sonDentifrice.wav", UriKind.Relative));
            verreSon.Open(new Uri(@"Resources\sonVerre.wav", UriKind.Relative));

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();

            this._startServer();

            string localIp = this._getLocalIPAddress();

            Console.WriteLine("http://" + localIp + ":" + this._serverPort.ToString());

            this._sm = new SocketManager("http://localhost:" + this._serverPort.ToString());

        }

        private void _startServer()
        {
            this._serverProcess = new Process();
            this._serverProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            this._serverProcess.StartInfo.CreateNoWindow = false;
            this._serverProcess.StartInfo.UseShellExecute = false;
            this._serverProcess.StartInfo.FileName = "cmd.exe";
            this._serverProcess.StartInfo.Arguments = "/c cd ../../../PaintServer/PaintServer/ & node PaintServer.js";
            this._serverProcess.EnableRaisingEvents = true;
            this._serverProcess.Start();
        }

        private string _getLocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            this._serverProcess.CloseMainWindow();

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        private  void touchez_Click(object sender, RoutedEventArgs e)
        {
            myGrid.Visibility = Visibility.Hidden;
            maison.Visibility = Visibility.Visible;

            animeMaison();
        }

        private async void animeMaison()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.To = 1.2;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.AutoReverse = true;
            cuisineScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            cuisineScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            cuisine.Play();
            await Task.Delay(2000);
            salonScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            salonScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            salon.Play();
            await Task.Delay(2000);
            salledebainScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            salledebainScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            salledebain.Play();
        }

        private async void animeSalleDeBain()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.To = 1.2;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.AutoReverse = true;
            brosseadentScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            brosseadentScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            brossezdent.Play();
            await Task.Delay(2000);
            brosseacheveuxScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            brosseacheveuxScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            coiffez.Play();
            await Task.Delay(2000);
            rasoirScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            rasoirScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            rasez.Play();
            await Task.Delay(2000);
            doucheScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            doucheScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            douchez.Play();
        }
        private void ScatterViewDrop(object sender, SurfaceDragDropEventArgs e)
        {

        }

        private void ScatterViewItemHoldGesture(object sender, TouchEventArgs e)
        {

        }

        private void salledabain_Click(object sender, RoutedEventArgs e)
        {
            maison.Visibility = Visibility.Hidden;
            atelier.Visibility = Visibility.Visible;
            animeSalleDeBain();
        }

        private async void brosseadent_Click(object sender, RoutedEventArgs e)
        {
            atelier.Visibility = Visibility.Hidden;
            objet.Visibility = Visibility.Visible;
            await Task.Delay(3000);
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(@"Resources/brosseadents.png", UriKind.Relative));
            to.Fill = myBrush;
            dentifriceImage.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative));
            verreImage.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative));
            await Task.Delay(3000);
             myBrush.ImageSource= new BitmapImage(new Uri(@"Resources/brosse_grandT.png", UriKind.Relative));
             to.Fill = myBrush;
            dentifriceImage.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative));
            verreImage.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative));
            await Task.Delay(3000);
            brosseadentSon.Play();
            dentifriceSon.Play();
            verreSon.Play();
        }

        private void brosseacheveux_Click(object sender, RoutedEventArgs e)
        {

        }

        private void douche_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rasoir_Click(object sender, RoutedEventArgs e)
        {

        }
        private void OnVisualizationAdded(object sender, TagVisualizerEventArgs e)
        {
            switch(e.TagVisualization.VisualizedTag.Value){
                case 1: Trace.WriteLine("brosse a dents"); to.Stroke = new SolidColorBrush(Colors.Green); break;
                case 2: Trace.WriteLine("dentifrice"); break;
                case 3: Trace.WriteLine("verre"); break;
                default: break;
            }

        }
    }
}