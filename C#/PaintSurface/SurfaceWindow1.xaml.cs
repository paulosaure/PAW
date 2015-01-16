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

        }
        public  void aide(String str)
        {
            this.Dispatcher.Invoke((Action)(() =>
    {
       
    
            String[] order = str.Split(' ');
            switch (order[0])
            {
                case "all": switch (order[1]) { case "texte": texteAide(); break; case "image": imageAide(); break; case "son": sonAide(); break; } break;
                case "dentifrice": switch (order[1])
                    {
                        case "texte": dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative)); dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative)); break;
                        case "image": dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative)); break;
                        case "son": son.Open(new Uri(@"Resources\sonDentifrice.wav", UriKind.Relative));
                            son.Play(); break;
                    } break;
                case "verre": switch (order[1])
                    {
                        case "texte": verre2.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative)); verre2.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative)); verre.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative)); break;
                        case "image": verre.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative)); verre2.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative)); break;
                        case "son": son.Open(new Uri(@"Resources\sonVerre.wav", UriKind.Relative));
                            son.Play(); break;
                    } break;
                case "brosse": switch (order[1])
                    {
                        case "texte": brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosseadents.png", UriKind.Relative)); brosseDent.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative)); break;
                        case "image": brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative)); brosseDent.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative)); break;
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
        private void ScatterViewDrop(object sender, SurfaceDragDropEventArgs e)
        {
            ImageSource i = new BitmapImage(new Uri(e.Cursor.Data as String));

            fleche.Source = i;
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

        private  void brosseadent_Click(object sender, RoutedEventArgs e)
        {
            atelier.Visibility = Visibility.Hidden;
            objet.Visibility = Visibility.Visible;
            
            //Objet en Texte
            /*
            await Task.Delay(3000);
            brosseDent.Source = new BitmapImage(new Uri("/Resources/brosseadents.png", UriKind.Relative));
            dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative));
            verre.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative));
            brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosseadents.png", UriKind.Relative));
            dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative));
            verre2.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative));

            //Objet en Image
            await Task.Delay(3000);
            brosseDent.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative));
            dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative));
            verre.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative));
            brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative));
            dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative));
            verre2.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative));
            //Objet Son
            await Task.Delay(3000);
            try
            {
                brosseadentSon.Play();
            }
            catch (System.NullReferenceException) { }

            await Task.Delay(2000);
            try
            {
                dentifriceSon.Play();
            }
            catch (System.NullReferenceException) { }

            await Task.Delay(2000);
            try
            {
                verreSon.Play();
            }
            catch (System.NullReferenceException) { }*/
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
        private void valideObjet(){

            if (brosseadentBool && verreBool && dentifriceBool)
            {
                aideTop.Visibility = Visibility.Hidden;
                aideBot.Visibility = Visibility.Hidden;
                ordonnancement.Visibility = Visibility.Visible;
                ordonnacement = true;
            }
        }
        private void createImageVerre(Point p)
        {
             i = new Image();
            i.Width = 200;
            i.Height = 200;
            i.Source = new BitmapImage(new Uri("/Resources/rincer_bouche.png", UriKind.Relative));
            i.SetValue(Canvas.LeftProperty, p.X-110);
            i.SetValue(Canvas.TopProperty, p.Y-230);
            i.TouchMove += i_TouchMove;
            canvas.Children.Add(i);

            i2 = new Image();
            i2.Width = 200;
            i2.Height = 200;
            i2.Source = new BitmapImage(new Uri("/Resources/cracher.png", UriKind.Relative));
            i2.SetValue(Canvas.LeftProperty, p.X-110);
            i2.SetValue(Canvas.TopProperty, p.Y+30);
            i2.TouchMove += i2_TouchMove;
            canvas.Children.Add(i2);
        }

        private void i_TouchMove(object sender, TouchEventArgs e)
        {
            image = 1;
            drop = true;
        }

        private void i2_TouchMove(object sender, TouchEventArgs e)
        {
            image = 2;
            drop = true;
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
        void i3_TouchDown(object sender, TouchEventArgs e)
        {
            Trace.WriteLine("touch down img");
            imgTmp.Margin = new Thickness(50, 50, 50, 50);
            imgTmp.Width = i3.Width+100;
            imgTmp.Height = i3.Height+100;
            imgTmp.Source = i3.Source;
            TouchPoint p = e.GetTouchPoint(this.canvas);
            imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i3));
            imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i3));
            imgTmp.TouchMove += imgTmp_TouchMove;
            canvas.Children.Remove(i3);
            canvas.Children.Add(imgTmp);

            image = 3;
            drop = true;
            
        }
        private bool ordonnacement = false;
        private void imgTmp_TouchMove(object sender, TouchEventArgs e)
        
        {
            if (ordonnacement)
            {
                Trace.WriteLine(" ENTRE  MOUVE CLONE");
                TouchPoint p = e.GetTouchPoint(this.canvas);
                Trace.WriteLine(p.Position);
                imgTmp.SetValue(Canvas.LeftProperty, p.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, p.Position.Y);
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
            i4.TouchMove += i4_TouchMove;
            canvas.Children.Add(i4);

            i5 = new Image();
            i5.Width = 200;
            i5.Height = 200;
            i5.Source = new BitmapImage(new Uri("/Resources/brosser.jpg", UriKind.Relative));
            i5.SetValue(Canvas.LeftProperty, p.X - 280);
            i5.SetValue(Canvas.TopProperty, p.Y - 130);
            i5.TouchMove += i5_TouchMove;
            canvas.Children.Add(i5);

            i6 = new Image();
            i6.Width = 200;
            i6.Height = 200;
            i6.Source = new BitmapImage(new Uri("/Resources/prendre_brossedent.png", UriKind.Relative));
            i6.SetValue(Canvas.LeftProperty, p.X - 110);
            i6.SetValue(Canvas.TopProperty, p.Y + 100);
            i6.TouchMove += i6_TouchMove;
            canvas.Children.Add(i6);
        }

        private void i6_TouchMove(object sender, TouchEventArgs e)
        {
            image = 6;
            drop = true; ;
        }

        private void i5_TouchMove(object sender, TouchEventArgs e)
        {
            image = 5;
            drop = true;
        }

        private void i4_TouchMove(object sender, TouchEventArgs e)
        {
            image = 4;
            drop = true;
        }
        private void OnVisualizationAdded(object sender, TagVisualizerEventArgs e)
        {
            Point p=e.TagVisualization.Center;

            Point t = new Point(p.X, p.Y+210);

            try
            {
                switch (e.TagVisualization.VisualizedTag.Value)
                {
                    case 0x01: createImageBrosse(t); borderAideBrosseDent.BorderBrush = Brushes.LightGreen; borderAideBrosseDent2.BorderBrush = Brushes.LightGreen; brosseadentBool = true; valideObjet(); break;
                    case 0x20: createImageDentifrice(t); borderDentifrice.BorderBrush = Brushes.LightGreen; borderDentifrice2.BorderBrush = Brushes.LightGreen; borderDentifrice.Visibility = Visibility.Visible; dentifriceBool = true; valideObjet(); break;
                    case 0xC5: createImageVerre(t); borderVerre.BorderBrush = Brushes.LightGreen; borderVerre2.BorderBrush = Brushes.LightGreen; borderVerre.Visibility = Visibility.Visible; verreBool = true; valideObjet(); break;
                    default: break;
                }
            }
            catch (System.Exception ex) { Trace.WriteLine("exeption "+ex); }
        }


       /* private void List_MouseMove(object sender, MouseEventArgs e)
        {
            if (t) { 
            Point mousePos = e.GetPosition(null);
            Point position = ActionBrosser.PointToScreen(new Point(0d, 0d));
            DoubleAnimation da = new DoubleAnimation();
            //Trace.WriteLine("PosX= " + mousePos.X + " PosY = " + mousePos.Y);
            //Trace.WriteLine("ImagePosX= " +position.X + " ImagePosY = " + position.Y);
                // Get the dragged ListViewItem
                Image img = sender as Image;
                DataObject data = new DataObject(typeof(ImageSource), img.Source);
                DragDrop.DoDragDrop(img, data, DragDropEffects.Move);
        }
        }*/

        private void DropList_Drop(object sender, DragEventArgs e)
        {
            Trace.WriteLine("test drop");
            Image img = sender as Image;
            if (img != null)
            {
                // Save the current Fill brush so that you can revert back to this value in DragLeave.
   

                // If the DataObject contains string data, extract it.
                if (e.Data.GetData(typeof(ImageSource)) != null)
                {
                    //Trace.WriteLine("Entre DROP 2");
                    ImageSource image = e.Data.GetData(typeof(ImageSource)) as ImageSource;

                    img.Source = image;
                }
            }
        }

        private void img_GiveFeedback(object sender, System.Windows.GiveFeedbackEventArgs e)
        {

        }

       /* private void mouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            if (img != null)
            {
                Point mousePos = e.GetPosition(null);
                Point position = ActionBrosser.PointToScreen(new Point(0d, 0d));
                DoubleAnimation da = new DoubleAnimation();
                //Trace.WriteLine("PosX= " + mousePos.X + " PosY = " + mousePos.Y);
                //Trace.WriteLine("ImagePosX= " + position.X + " ImagePosY = " + position.Y);
                t = true;
            }
        }*/

        private void mouseUp(object sender, MouseButtonEventArgs e)
        {
            
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

        private async void brosseadent_Touch(object sender, TouchEventArgs e)
        {

            atelier.Visibility = Visibility.Hidden;
            objet.Visibility = Visibility.Visible;
            /*
            //Objet en Texte
            await Task.Delay(3000);
            brosseDent.Source = new BitmapImage(new Uri("/Resources/brosseadents.png", UriKind.Relative));
            dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative));
            verre.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative));
            brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosseadents.png", UriKind.Relative));
            dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative));
            verre2.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative));

            //Objet en Image
            await Task.Delay(3000);
            brosseDent.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative));
            dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative));
            verre.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative));
            brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative));
            dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative));
            verre2.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative));
            //Objet Son
            await Task.Delay(3000);
            try
            {
                brosseadentSon.Play();
            }
            catch (System.NullReferenceException) { }

            await Task.Delay(2000);
            try
            {
                dentifriceSon.Play();
            }
            catch (System.NullReferenceException) { }

            await Task.Delay(2000);
            try
            {
                verreSon.Play();
            }
            catch (System.NullReferenceException) { }*/
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

        private bool drop = false;
        private bool touchSurFrise = false;
        private void touchTEST(object sender, TouchEventArgs e)
        {
            Trace.WriteLine("touh up frise");
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
                canvas.Children.Remove(imgTmp);
                drop = false;
                touchSurFrise = true;
            }
        }

        private void mouveDelete(object sender, TouchEventArgs e)
        {
            touchSurFrise = true;
            Image img = sender as Image;
            img.Source = new BitmapImage(new Uri("/Resources/elt.png", UriKind.Relative));
            
        }
  private void imgTmp_TouchUp(object sender, TouchEventArgs e)
  {
      Trace.WriteLine("canvas touch up");
            if (ordonnacement && !touchSurFrise)
            {
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
    }
}


