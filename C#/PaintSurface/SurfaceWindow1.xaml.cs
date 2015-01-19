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
using System.ComponentModel;

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

        private MediaPlayer son = new MediaPlayer();
        private bool drop = false;
        private bool touchSurFrise = false;
        private int[] trueOrder;
        private int[] orderFriseHaut;
        private int[] orderFriseBas;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(Window1_Closing);

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();

            this._startServer();



            string localIp = this._getLocalIPAddress();



            //Console.WriteLine("http://" + localIp + ":" + this._serverPort.ToString());



            this._sm = new SocketManager("http://localhost:" + this._serverPort.ToString(),this);
            trueOrder = new int[6] { 6, 4,3, 5, 2, 1};
            orderFriseBas = new int[6];
            orderFriseHaut = new int[6];

        }
        private bool aideDentifrice = false;
        private bool aideBrosse = false;
        private bool aideVerre = false;
        public  void aide(String str)
        {
            this.Dispatcher.Invoke((Action)(() =>
    {
       
    
            String[] order = str.Split(' ');
            switch (order[0])
            {
                case "all": aideBool = true; switch (order[1]) { case "texte": texteAide(); break; case "image": imageAide(); break; case "son": sonAide(); break; } break;
                case "dentifrice": switch (order[1])
                    {
                        case "texte": aideDentifrice = true; dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative)); dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative)); break;
                        case "image": aideDentifrice = true; dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative)); dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative)); break;
                        case "son": son.Open(new Uri(@"Resources\sonDentifrice.wav", UriKind.Relative));
                            son.Play(); break;
                    } break;
                case "verre": switch (order[1])
                    {
                        case "texte": aideVerre = true; verre2.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative)); verre.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative)); break;
                        case "image": aideVerre = true; verre.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative)); verre2.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative)); break;
                        case "son": son.Open(new Uri(@"Resources\sonVerre.wav", UriKind.Relative));
                            son.Play(); break;
                    } break;
                case "brosse": switch (order[1])
                    {
                        case "texte": aideBrosse = true; brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosseadents.png", UriKind.Relative)); brosseDent.Source = new BitmapImage(new Uri("/Resources/brosseadents.png", UriKind.Relative)); break;
                        case "image": aideBrosse = true; brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative)); brosseDent.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative)); break;
                        case "son": son.Open(new Uri(@"Resources\sonBrosseDent.wav", UriKind.Relative));
                            son.Play(); break;
                    } break;
            }

    }));
        }
        private  void texteAide()
        {

            brosseDent.Source = new BitmapImage(new Uri("/Resources/brosseadents.png", UriKind.Relative));
            dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative));
            verre.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative));
            brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosseadents.png", UriKind.Relative));
            dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative));
            verre2.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative));
        }

        private void imageAide()
        {
            brosseDent.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative));
            dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative));
            verre.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative));
            brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative));
            dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative));
            verre2.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative));
        }
        private async void sonAide()
        {
            Trace.WriteLine("SON AIDE");
            await Task.Delay(1000);
            son.Open(new Uri(@"Resources\sonBrosseDent.wav", UriKind.Relative));
            son.Play();

            await Task.Delay(2000);
            son.Open(new Uri(@"Resources\sonDentifrice.wav", UriKind.Relative));
            son.Play();

            await Task.Delay(2000);
            son.Open(new Uri(@"Resources\sonVerre.wav", UriKind.Relative));
            son.Play();
            await Task.Delay(1000);
          
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

        private void touchez_Click(object sender, RoutedEventArgs e)
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

            await Task.Delay(1000);
            son.Open(new Uri(@"Resources\cuisine.wav", UriKind.Relative));
            son.Play();
            cuisineScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            cuisineScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);

            await Task.Delay(2000);
            son.Open(new Uri(@"Resources\salon.wav", UriKind.Relative));
            son.Play();
            salonScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            salonScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);

            await Task.Delay(2000);
            son.Open(new Uri(@"Resources\salledebain.wav", UriKind.Relative));
            son.Play();
            salledebainScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            salledebainScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);

        }

        private async void animeSalleDeBain()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.To = 1.2;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.AutoReverse = true;

            await Task.Delay(1000);
            brosseadentScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            brosseadentScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            son.Open(new Uri(@"Resources\brossezlesdents.wav", UriKind.Relative));
            son.Play();

            await Task.Delay(2000);
            brosseacheveuxScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            brosseacheveuxScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            son.Open(new Uri(@"Resources\coiffez.wav", UriKind.Relative));
            son.Play();
            await Task.Delay(2000);
            rasoirScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            rasoirScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            son.Open(new Uri(@"Resources\rasez.wav", UriKind.Relative));
            son.Play();
            await Task.Delay(2000);
            doucheScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            doucheScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            son.Open(new Uri(@"Resources\douchez.wav", UriKind.Relative));
            son.Play();
        }


        private void salledabain_Click(object sender, RoutedEventArgs e)
        {
            maison.Visibility = Visibility.Hidden;
            atelier.Visibility = Visibility.Visible;
            animeSalleDeBain();
        }

        private  void brosseadent_Click(object sender, RoutedEventArgs e)
        {
            atelier.Visibility = Visibility.Hidden;
            objet.Visibility = Visibility.Visible;
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
        private async void valideObjet(){

            if (brosseadentBool && verreBool && dentifriceBool)
            {
                aideTop.Visibility = Visibility.Hidden;
                aideBot.Visibility = Visibility.Hidden;
                ordonnancement.Visibility = Visibility.Visible;
                ordonnacement = true;
                text.Text = "Ordonnacer les actions";
                text2.Text = "Ordonnacer les actions";
            }
        }
        private void rotateAllImage()
        {
            DoubleAnimation rotateAnimation = new DoubleAnimation(0, 180, TimeSpan.FromSeconds(1));
            i.RenderTransform = new RotateTransform();
            i.RenderTransformOrigin = new Point(0.5, 0.5);
            RotateTransform rt = (RotateTransform)i.RenderTransform;
            rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            i2.RenderTransform = new RotateTransform();
            i2.RenderTransformOrigin = new Point(0.5, 0.5);
             rt = (RotateTransform)i2.RenderTransform;
            rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            i3.RenderTransform = new RotateTransform();
            i3.RenderTransformOrigin = new Point(0.5, 0.5);
             rt = (RotateTransform)i3.RenderTransform;
            rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            i4.RenderTransform = new RotateTransform();
            i4.RenderTransformOrigin = new Point(0.5, 0.5);
            rt = (RotateTransform)i4.RenderTransform;
            rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            i5.RenderTransform = new RotateTransform();
            i5.RenderTransformOrigin = new Point(0.5, 0.5);
             rt = (RotateTransform)i5.RenderTransform;
            rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            i6.RenderTransform = new RotateTransform();
            i6.RenderTransformOrigin = new Point(0.5, 0.5);
             rt = (RotateTransform)i6.RenderTransform;
            rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

        }
        private void createImageVerre(Point p)
        {
             i = new Image();
            i.Width = 200;
            i.Height = 200;
            i.Source = new BitmapImage(new Uri("/Resources/rincer_bouche.png", UriKind.Relative));
            i.SetValue(Canvas.LeftProperty, p.X-110);
            i.SetValue(Canvas.TopProperty, p.Y-230);
            i.TouchDown += i_TouchDown;
            canvas.Children.Add(i);

            i2 = new Image();
            i2.Width = 200;
            i2.Height = 200;
            i2.Source = new BitmapImage(new Uri("/Resources/cracher.png", UriKind.Relative));
            i2.SetValue(Canvas.LeftProperty, p.X-110);
            i2.SetValue(Canvas.TopProperty, p.Y+30);
            i2.TouchDown += i2_TouchDown;
            canvas.Children.Add(i2);
        }

        private Image i,i2,i3,i4,i5,i6;
        private void createImageDentifrice(Point p)
        {
            i3 = new Image();
            i3.Width = 200;
            i3.Height = 200;
            i3.Source = new BitmapImage(new Uri("/Resources/mettre_dentifrice.png", UriKind.Relative));
            i3.SetValue(Canvas.LeftProperty, p.X - 110);
            i3.SetValue(Canvas.TopProperty, p.Y - 230);
            i3.TouchDown += i3_TouchDown;
            canvas.Children.Add(i3);

        }
        private int image=-1;
        private Image imgTmp = new Image();
        private bool touchDownImage = false;
        private bool simpleTouch = false;

        void i3_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                Trace.WriteLine("touch down img");
                TouchPoint p2 = e.GetTouchPoint(i3);
                Trace.WriteLine("Position touch down i3 = " + p2.Position);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i3.Width;
                imgTmp.Height = i3.Height;
                imgTmp.Source = i3.Source;
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i3)-p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i3)-p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i3);
                canvas.Children.Add(imgTmp);
                touchDownImage = true;
                image = 3;
                drop = true;
            }
            
        }
        void i2_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                Trace.WriteLine("touch down img");
                TouchPoint p2 = e.GetTouchPoint(i2);
                Trace.WriteLine("Position touch down i2 = " + p2.Position);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i2.Width;
                imgTmp.Height = i2.Height;
                imgTmp.Source = i2.Source;
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i2) - p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i2) - p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i2);
                canvas.Children.Add(imgTmp);
                touchDownImage = true;
                image = 2;
                drop = true;
            }

        }
        void i_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                Trace.WriteLine("touch down img");
                TouchPoint p2 = e.GetTouchPoint(i);
                Trace.WriteLine("Position touch down i = " + p2.Position);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i.Width;
                imgTmp.Height = i.Height;
                imgTmp.Source = i.Source;
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i) - p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i) - p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i);
                canvas.Children.Add(imgTmp);
                touchDownImage = true;
                image = 1;
                drop = true;
            }

        }

        void i4_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                Trace.WriteLine("touch down img");
                TouchPoint p2 = e.GetTouchPoint(i4);
                Trace.WriteLine("Position touch down i4 = " + p2.Position);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i4.Width;
                imgTmp.Height = i4.Height;
                imgTmp.Source = i4.Source;
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i4) - p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i4) - p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i4);
                canvas.Children.Add(imgTmp);
                touchDownImage = true;
                image = 4;
                drop = true;
            }

        }
        void i5_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                Trace.WriteLine("touch down img");
                TouchPoint p2 = e.GetTouchPoint(i5);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i5.Width;
                imgTmp.Height = i5.Height;
                imgTmp.Source = i5.Source;
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i5) - p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i5) - p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i5);
                canvas.Children.Add(imgTmp);
                touchDownImage = true;
                image = 5;
                drop = true;
            }
        }

        void i6_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement)
            {
                simpleTouch = true;
                TouchPoint p2 = e.GetTouchPoint(i6);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i6.Width;
                imgTmp.Height = i6.Height;
                imgTmp.Source = i6.Source;
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i6) - p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i6) - p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i6);
                canvas.Children.Add(imgTmp);
                touchDownImage = true;
                image = 6;
                drop = true;
            }

        }
        private bool ordonnacement = false;
        private void imgTmp_TouchMove(object sender, TouchEventArgs e)
        
        {
            if (ordonnacement)
            {
                TouchPoint p = e.GetTouchPoint(this.canvas);
                Trace.WriteLine(p.Position);
                imgTmp.SetValue(Canvas.LeftProperty, p.Position.X-40 );
                imgTmp.SetValue(Canvas.TopProperty, p.Position.Y-40 );
            }
        }
       
        private void createImageBrosse(Point p)
        {
            i4 = new Image();
            i4.Width = 200;
            i4.Height = 200;
            i4.Source = new BitmapImage(new Uri("/Resources/mouiller_brosse.png", UriKind.Relative));
            i4.SetValue(Canvas.LeftProperty, p.X +110);
            i4.SetValue(Canvas.TopProperty, p.Y - 130);
            i4.TouchDown += i4_TouchDown;
            canvas.Children.Add(i4);

            i5 = new Image();
            i5.Width = 200;
            i5.Height = 200;
            i5.Source = new BitmapImage(new Uri("/Resources/brosser.jpg", UriKind.Relative));
            i5.SetValue(Canvas.LeftProperty, p.X - 280);
            i5.SetValue(Canvas.TopProperty, p.Y - 130);
            i5.TouchDown += i5_TouchDown;
            canvas.Children.Add(i5);

            i6 = new Image();
            i6.Width = 200;
            i6.Height = 200;
            i6.Source = new BitmapImage(new Uri("/Resources/prendre_brossedent.png", UriKind.Relative));
            i6.SetValue(Canvas.LeftProperty, p.X - 110);
            i6.SetValue(Canvas.TopProperty, p.Y + 100);
            i6.TouchDown += i6_TouchDown;
            canvas.Children.Add(i6);
        }

     

        private bool aideBool = false;
        private void OnVisualizationAdded(object sender, TagVisualizerEventArgs e)
        {
            Point p=e.TagVisualization.Center;
            Point t = new Point(p.X, p.Y+210);
            try
            {
                switch (e.TagVisualization.VisualizedTag.Value)
                {
                    case 0x01: createImageBrosse(t); if (aideBool||aideBrosse) { borderAideBrosseDent.BorderBrush = Brushes.LightGreen; borderAideBrosseDent2.BorderBrush = Brushes.LightGreen; } brosseadentBool = true; valideObjet(); break;
                    case 0x20: createImageDentifrice(t); if (aideBool||aideDentifrice) { borderDentifrice.BorderBrush = Brushes.LightGreen; borderDentifrice2.BorderBrush = Brushes.LightGreen; } borderDentifrice.Visibility = Visibility.Visible; dentifriceBool = true; valideObjet(); break;
                    case 0xC5: createImageVerre(t); if (aideBool||aideVerre) { borderVerre.BorderBrush = Brushes.LightGreen; borderVerre2.BorderBrush = Brushes.LightGreen; } borderVerre.Visibility = Visibility.Visible; verreBool = true; valideObjet(); break;
                    default: break;
                }
            }
            catch (System.Exception ex) { Trace.WriteLine("exeption "+ex); }
        }



        void Window1_Closing(object sender, CancelEventArgs e)
        {

            Application.Current.Shutdown();

        }

        private void touch(object sender, TouchEventArgs e)
        {
            myGrid.Visibility = Visibility.Hidden;
            maison.Visibility = Visibility.Visible;
            animeMaison();
        }

        private  void brosseadent_Touch(object sender, TouchEventArgs e)
        {

            atelier.Visibility = Visibility.Hidden;
            objet.Visibility = Visibility.Visible;
           
        }

        private void salledebain_Touch(object sender, TouchEventArgs e)
        {
            maison.Visibility = Visibility.Hidden;
            atelier.Visibility = Visibility.Visible;
            animeSalleDeBain();
        }

        private void deleteImageBrosse(Point p){
            canvas.Children.Remove(i4);
            canvas.Children.Remove(i5);
            canvas.Children.Remove(i6);
        }
        private void deleteImageDentifrice(Point p){
            canvas.Children.Remove(i3);
        }
        private void deleteImageVerre(Point p){
            canvas.Children.Remove(i);
            canvas.Children.Remove(i2);
        }
        private void OnvisualEnd(object sender, TagVisualizerEventArgs e)
        {
            Point p = e.TagVisualization.Center;
            Point t = new Point(p.X, p.Y + 210);
            try
            {
                switch (e.TagVisualization.VisualizedTag.Value)
                {
                    case 0x01: deleteImageBrosse(t); borderAideBrosseDent.BorderBrush = Brushes.Transparent; borderAideBrosseDent2.BorderBrush = Brushes.Transparent; brosseadentBool = false; break;
                    case 0x20: deleteImageDentifrice(t); borderDentifrice.BorderBrush = Brushes.Transparent; borderDentifrice2.BorderBrush = Brushes.Transparent; dentifriceBool = false; break;
                    case 0xC5: deleteImageVerre(t); borderVerre.BorderBrush = Brushes.Transparent; borderVerre2.BorderBrush = Brushes.Transparent; verreBool = false; break;
                    default: break;
                }
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine("exeption " + ex);
            }
        }

     

        private void moveImageVerre(Point p)
        {
            i.SetValue(Canvas.LeftProperty, p.X - 110);
            i.SetValue(Canvas.TopProperty, p.Y - 230);
            i2.SetValue(Canvas.LeftProperty, p.X - 110);
            i2.SetValue(Canvas.TopProperty, p.Y + 30);
        }

        private void moveImageDentifrice(Point p)
        {
            i3.SetValue(Canvas.LeftProperty, p.X - 110);
            i3.SetValue(Canvas.TopProperty, p.Y - 230);
        }

        private void moveImageBrosse(Point p)
        {
            i4.SetValue(Canvas.LeftProperty, p.X + 110);
            i4.SetValue(Canvas.TopProperty, p.Y - 130);
            i5.SetValue(Canvas.LeftProperty, p.X - 280);
            i5.SetValue(Canvas.TopProperty, p.Y - 130);
            i6.SetValue(Canvas.LeftProperty, p.X - 110);
            i6.SetValue(Canvas.TopProperty, p.Y + 100);
        }


        private void touchTEST(object sender, TouchEventArgs e)
        {
            simpleTouch = false;
            if (drop)
            {
                Image img = sender as Image;
                switch (image) {
                    case 1: img.Source = new BitmapImage(new Uri("/Resources/rincer_bouche.png", UriKind.Relative));canvas.Children.Add(i);break;
                    case 2: img.Source = new BitmapImage(new Uri("/Resources/cracher.png", UriKind.Relative));canvas.Children.Add(i2); break;
                    case 3: img.Source = new BitmapImage(new Uri("/Resources/mettre_dentifrice.png", UriKind.Relative));canvas.Children.Add(i3); break;
                    case 4: img.Source = new BitmapImage(new Uri("/Resources/mouiller_brosse.png", UriKind.Relative)); canvas.Children.Add(i4);break;
                    case 5: img.Source = new BitmapImage(new Uri("/Resources/brosser.jpg", UriKind.Relative)); canvas.Children.Add(i5);break;
                    case 6: img.Source = new BitmapImage(new Uri("/Resources/prendre_brossedent.png", UriKind.Relative));canvas.Children.Add(i6); break;
                    default: break;
            }
                entreOrdre(img);
                canvas.Children.Remove(imgTmp);
                drop = false;
                touchSurFrise = true;
                touchDownImage = false;
            }
        }

        private void entreOrdre(Image img)
        {
            switch (img.Name)
            {
                case "bloc1": orderFriseHaut[0] = image; if (image == trueOrder[0]) borderbloc1.BorderBrush = Brushes.LightGreen; break;
                case "bloc2": orderFriseHaut[1] = image; if (image == trueOrder[1]) borderbloc2.BorderBrush = Brushes.LightGreen; break;
                case "bloc3": orderFriseHaut[2] = image; if (image == trueOrder[2]) borderbloc3.BorderBrush = Brushes.LightGreen; break;
                case "bloc4": orderFriseHaut[3] = image; if (image == trueOrder[3]) borderbloc4.BorderBrush = Brushes.LightGreen; break;
                case "bloc5": orderFriseHaut[4] = image; if (image == trueOrder[4]) borderbloc5.BorderBrush = Brushes.LightGreen; break;
                case "bloc6": orderFriseHaut[5] = image; if (image == trueOrder[5]) borderbloc6.BorderBrush = Brushes.LightGreen; break;
                case "bloc1B": orderFriseBas[0] = image; if (image == trueOrder[0]) borderbloc7.BorderBrush = Brushes.LightGreen; break;
                case "bloc2B": orderFriseBas[1] = image; if (image == trueOrder[1]) borderbloc8.BorderBrush = Brushes.LightGreen; break;
                case "bloc3B": orderFriseBas[2] = image; if (image == trueOrder[2]) borderbloc9.BorderBrush = Brushes.LightGreen; break;
                case "bloc4B": orderFriseBas[3] = image; if (image == trueOrder[3]) borderbloc10.BorderBrush = Brushes.LightGreen; break;
                case "bloc5B": orderFriseBas[4] = image; if (image == trueOrder[4]) borderbloc11.BorderBrush = Brushes.LightGreen; break;
                case "bloc6B": orderFriseBas[5] = image; if (image == trueOrder[5]) borderbloc12.BorderBrush = Brushes.LightGreen; break;
            }
            testOrdre();
        }

        private bool ordreFriseHaut = false, ordreFriseBas = false;
        private void testOrdre()
        {
            ordreFriseBas = true;
            ordreFriseHaut = true;
            int i = 0;
            for (i = 0; i < 6; i++)
            {
                if (trueOrder[i] != orderFriseHaut[i])
                {
                    ordreFriseHaut = false; 
                    break;
                }
                if (trueOrder[i] != orderFriseBas[i])
                {
                    ordreFriseBas = false; 
                    break;
                }
                
            }
            if(ordreFriseHaut && ordreFriseBas)
                Trace.WriteLine("SUCCES BRAVO");
            else
                Trace.WriteLine("Pas encore");
        }
        private void mouveDelete(object sender, TouchEventArgs e)
        {
            touchSurFrise = true;
            Image img = sender as Image;
            img.Source = new BitmapImage(new Uri("/Resources/elt.png", UriKind.Relative));
            
        }
  private void imgTmp_TouchUp(object sender, TouchEventArgs e)
  {
      simpleTouch = false;
      drop = false;
            if (ordonnacement && !touchSurFrise && touchDownImage)
            {
                touchDownImage = false;
                switch (image)
                {
                    case 1:  canvas.Children.Add(i); break;
                    case 2:  canvas.Children.Add(i2); break;
                    case 3:  canvas.Children.Add(i3); break;
                    case 4:  canvas.Children.Add(i4); break;
                    case 5:  canvas.Children.Add(i5); break;
                    case 6:  canvas.Children.Add(i6); break;
                    default: break;
                }
                canvas.Children.Remove(imgTmp);
            }
            else
            {
                touchSurFrise = false;
            }
        }

  private void OnvisualMoved(object sender, TagVisualizerEventArgs e)
  {

      Point p = e.TagVisualization.Center;

      Point t = new Point(p.X, p.Y + 210);

      try
      {

          switch (e.TagVisualization.VisualizedTag.Value)
          {

              case 0x01: moveImageBrosse(t); borderAideBrosseDent.BorderBrush = Brushes.LightGreen; borderAideBrosseDent2.BorderBrush = Brushes.LightGreen; break;

              case 0x20: moveImageDentifrice(t); borderDentifrice.BorderBrush = Brushes.LightGreen; borderDentifrice2.BorderBrush = Brushes.LightGreen; break;

              case 0xC5: moveImageVerre(t); borderVerre.BorderBrush = Brushes.LightGreen; borderVerre2.BorderBrush = Brushes.LightGreen; break;

              default: break;

          }

      }

      catch (System.Exception ex)
      {

          Trace.WriteLine("exeption " + ex);

      }

  }
    }
}


