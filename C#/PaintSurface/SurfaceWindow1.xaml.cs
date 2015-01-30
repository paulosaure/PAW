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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

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
        public int vueCourante = 1; //1= roue 2=piece 3=atelier 4= objet 5=ordonnancement 6=video
        private bool aideDentifrice = false;
        private bool aideBrosse = false;
        private bool aideVerre = false;
        private int image = -1;
        private Image imgTmp = new Image();
        private bool touchDownImage = false;
        private bool simpleTouch = false;
        private bool imageBloc1 = true, imageBloc1B = true, imageBloc2 = true, imageBloc2B = true, imageBloc3 = true, imageBloc3B = true, imageBloc4 = true, imageBloc4B = true, imageBloc5 = true, imageBloc5B = true, imageBloc6 = true, imageBloc6B = true;
        private Image i, i2, i3, i4, i5, i6;
        private Frise friseHautObjet, friseBasObjet;
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

            QrcodeBas.Source = QRCodeGenerator.generatorQRCodeForTableSide("left");
            QrcodeHaut.Source = QRCodeGenerator.generatorQRCodeForTableSide("right");
            friseBasObjet = new Frise() { atelier="brossage de dents",bloc1 = "image", bloc2 = "image", bloc3 = "image", bloc4 = "image" };
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Frise));
            MemoryStream ms = new MemoryStream();
            js.WriteObject(ms, friseBasObjet);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string s = sr.ReadToEnd();
            Trace.WriteLine("JSON : "+s);

            friseHautObjet = new Frise();
        }




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
                        case "texte": aideBrosse = true; brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosserText.png", UriKind.Relative)); brosseDent.Source = new BitmapImage(new Uri("/Resources/brosserText.png", UriKind.Relative)); break;
                        case "image": aideBrosse = true; brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative)); brosseDent.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative)); break;
                        case "son": son.Open(new Uri(@"Resources\sonBrosseDent.wav", UriKind.Relative));
                            son.Play(); break;
                    } break;
            }

    }));
        }

        public void soundRequete(String str)
        {
                     this.Dispatcher.Invoke((Action)(() =>
    {
            son.Stop();
            switch (str)
            {
                case "encouragement": son.Open(new Uri(@"Resources\encouragement.mp3", UriKind.Relative));son.Play(); break;
                case "essaieEncore": son.Open(new Uri(@"Resources\essaie_encore.mp3", UriKind.Relative)); son.Play(); break;
                case "felicitation": son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); break;
                case "prendreBrosse": son.Open(new Uri(@"Resources\prendreBrosse.wav", UriKind.Relative)); son.Play(); break;
                case "mouillerBrosse": son.Open(new Uri(@"Resources\mouillerBrosse.wav", UriKind.Relative)); son.Play(); break;
                case "mettreDentifrice": son.Open(new Uri(@"Resources\mettreDentifrice.wav", UriKind.Relative)); son.Play(); break;
                case "brosser": son.Open(new Uri(@"Resources\brosser.wav", UriKind.Relative)); son.Play(); break;
                case "rincer": son.Open(new Uri(@"Resources\rincer.wav", UriKind.Relative)); son.Play(); break;
                case "cracher": son.Open(new Uri(@"Resources\cracher.wav", UriKind.Relative)); son.Play(); break;
                default: break;
            }
    }));
        }

        public void clignoterRequete(string str)
        {
            this.Dispatcher.Invoke((Action)(() =>
{
    if (ordonnacement)
    {
        DoubleAnimation da = new DoubleAnimation();
        da.To = 1.2;
        da.Duration = new Duration(TimeSpan.FromSeconds(1));
        da.AutoReverse = true;

        switch (str)
        {
            case "dentifrice": if (dentifriceBool) createCircleDentifrice(dentifriceObjetPoint); break;
            case "verre": if (verreBool) createCircleVerre(verreObjetPoint); break;
            case "brosse": if (brosseadentBool) createCircleBrosse(brosseObjetPoint); break;
            case "prendreBrosse": ScaleTransform trans6 = new ScaleTransform();
                i6.RenderTransform = trans6;
                i6.RenderTransformOrigin = new Point(0.5, 0.5);
                trans6.BeginAnimation(ScaleTransform.ScaleXProperty, da);
                trans6.BeginAnimation(ScaleTransform.ScaleYProperty, da); break;
            case "mouillerBrosse": ScaleTransform trans4 = new ScaleTransform();
                i4.RenderTransform = trans4;
                i4.RenderTransformOrigin = new Point(0.5, 0.5);
                trans4.BeginAnimation(ScaleTransform.ScaleXProperty, da);
                trans4.BeginAnimation(ScaleTransform.ScaleYProperty, da); break;
            case "mettreDentifrice": ScaleTransform trans3 = new ScaleTransform();
                i3.RenderTransform = trans3;
                i3.RenderTransformOrigin = new Point(0.5, 0.5);
                trans3.BeginAnimation(ScaleTransform.ScaleXProperty, da);
                trans3.BeginAnimation(ScaleTransform.ScaleYProperty, da); break;
            case "brosser": ScaleTransform trans5 = new ScaleTransform();
                i5.RenderTransform = trans5;
                i5.RenderTransformOrigin = new Point(0.5, 0.5);
                trans5.BeginAnimation(ScaleTransform.ScaleXProperty, da);
                trans5.BeginAnimation(ScaleTransform.ScaleYProperty, da); break;
            case "rincer":
                ScaleTransform trans = new ScaleTransform();
                i.RenderTransform = trans;
                i.RenderTransformOrigin = new Point(0.5, 0.5);
                trans.BeginAnimation(ScaleTransform.ScaleXProperty, da);
                trans.BeginAnimation(ScaleTransform.ScaleYProperty, da); break;
            case "cracher": ScaleTransform trans2 = new ScaleTransform();
                i2.RenderTransform = trans2;
                i2.RenderTransformOrigin = new Point(0.5, 0.5);
                trans2.BeginAnimation(ScaleTransform.ScaleXProperty, da);
                trans2.BeginAnimation(ScaleTransform.ScaleYProperty, da); break;
            default: break;
        }
    }
}));
        }
        public void hardPushRequete(string str)
        {
            this.Dispatcher.Invoke((Action)(() =>
{
    son.Stop();
    if (str == "next")
    {
        if (vueCourante < 6)
            vueCourante++;
        switch (vueCourante)
        {
            case 1: break;
            case 2: myGrid.Visibility = Visibility.Hidden; maison.Visibility = Visibility.Visible; animeMaison(); break;
            case 3: maison.Visibility = Visibility.Hidden; atelier.Visibility = Visibility.Visible; animeSalleDeBain(); break;
            case 4: objetVue(); break;
            case 5: aideTop.Visibility = Visibility.Hidden;
                aideBot.Visibility = Visibility.Hidden;
                ordonnancement.Visibility = Visibility.Visible;
                rotatefecheBas.Visibility = Visibility.Visible;
                rotatefecheHaut.Visibility = Visibility.Visible;
                son.Open(new Uri(@"Resources\placerActions.wav", UriKind.Relative));
                son.Play();
                ordonnacement = true;
                text.Text = "Ordonnancer les actions";
                text2.Text = "Ordonnancer les actions"; break;
            case 6: dernièreVue = true;
                ordonnacement = false;
                ordonnancement.Visibility = Visibility.Visible;

                videoHaut.Visibility = Visibility.Visible;
                videoBas.Visibility = Visibility.Visible;
                friseBas.Visibility = Visibility.Hidden;
                friseHaut.Visibility = Visibility.Hidden;
                text.Text = "Touchez une image pour lancer la vidéo";
                text2.Text = "Touchez une image pour lancer la vidéo";
                Trace.WriteLine("SUCCES BRAVO");
                son.Open(new Uri(@"Resources\selectionnerImagePourVideo.wav", UriKind.Relative));
                son.Play(); break;
            default: break;
        }
    }
    else
    {
        if (vueCourante > 2)
            vueCourante--;
        switch (vueCourante)
        {
            case 1: break;
            case 2: atelier.Visibility = Visibility.Hidden; maison.Visibility = Visibility.Visible; animeMaison(); break;
            case 3: objet.Visibility = Visibility.Hidden; atelier.Visibility = Visibility.Visible; animeSalleDeBain(); break;
            case 4: objetVue(); ordonnacement = false; ordonnancement.Visibility = Visibility.Hidden;
                rotatefecheBas.Visibility = Visibility.Hidden;
                rotatefecheHaut.Visibility = Visibility.Hidden;
                                aideTop.Visibility = Visibility.Visible;
                aideBot.Visibility = Visibility.Visible;
                text.Text = "Placer les objets";
                text2.Text = "Placer les objets";
                                son.Open(new Uri(@"Resources\poserObjetBrossageDents.wav", UriKind.Relative));
                son.Play();break;
            case 5: dernièreVue = false;
                ordonnancement.Visibility = Visibility.Visible;
                rotatefecheBas.Visibility = Visibility.Visible;
                rotatefecheHaut.Visibility = Visibility.Visible;
                son.Open(new Uri(@"Resources\placerActions.wav", UriKind.Relative));
                son.Play();
                ordonnacement = true;
                ordonnancement.Visibility = Visibility.Visible;
                videoHaut.Visibility = Visibility.Hidden;
                videoBas.Visibility = Visibility.Hidden;
                friseBas.Visibility = Visibility.Visible;
                friseHaut.Visibility = Visibility.Visible;
                text.Text = "Ordonnancer les actions";
                text2.Text = "Ordonnancer les actions"; break;
            case 6: break;
            default: break;
        }
    }
}));
        }
        public void aideAtelierRequete(string str)
        {
            this.Dispatcher.Invoke((Action)(() =>
{
    son.Stop();
    DoubleAnimation da = new DoubleAnimation();
    da.To = 1.2;
    da.Duration = new Duration(TimeSpan.FromSeconds(1));
    da.AutoReverse = true;
    switch (str)
    {
        case "all": animeSalleDeBain(); break;
        case "brosserDents": brosseadentScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            brosseadentScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            son.Open(new Uri(@"Resources\brossezlesdents.wav", UriKind.Relative));
            son.Play(); break;
        case "brosserCheveux": brosseacheveuxScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            brosseacheveuxScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            son.Open(new Uri(@"Resources\coiffez.wav", UriKind.Relative));
            son.Play(); break;
        case "doucher": doucheScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            doucheScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            son.Open(new Uri(@"Resources\douchez.wav", UriKind.Relative));
            son.Play(); break;
        case "raser": rasoirScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            rasoirScale.BeginAnimation(ScaleTransform.ScaleYProperty, da);
            son.Open(new Uri(@"Resources\rasez.wav", UriKind.Relative));
            son.Play(); break;
        default: break;
    }
}));
        }
        public void aidePlaceRequete(string str)
        {
            this.Dispatcher.Invoke((Action)(() =>
{
    son.Stop();
    DoubleAnimation da = new DoubleAnimation();
    da.To = 1.2;
    da.Duration = new Duration(TimeSpan.FromSeconds(1));
    da.AutoReverse = true;
    switch (str)
    {
        case "all": animeMaison(); break;
        case "cuisine": son.Open(new Uri(@"Resources\cuisine.wav", UriKind.Relative));
            son.Play();
            cuisineScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            cuisineScale.BeginAnimation(ScaleTransform.ScaleYProperty, da); break;
        case "salleDeBain": son.Open(new Uri(@"Resources\salledebain.wav", UriKind.Relative));
            son.Play();
            salledebainScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            salledebainScale.BeginAnimation(ScaleTransform.ScaleYProperty, da); break;
        case "salon": son.Open(new Uri(@"Resources\salon.wav", UriKind.Relative));
            son.Play();
            salonScale.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            salonScale.BeginAnimation(ScaleTransform.ScaleYProperty, da); break;
        default: break;
    }
}));
        }
        public void aideActionRequete(string str)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                son.Stop();
                String[] order = str.Split(' ');
            switch (order[0])
            {
                case "all": switch (order[1]) { case "texte": actionTexte(); break; case "image": actionImage(); break; } break;
                case "prendreBrosse": switch (order[1]) { case "texte": i6.Source = new BitmapImage(new Uri("/Resources/prendreBrosseText.png", UriKind.Relative)); break; case "image": i6.Source = new BitmapImage(new Uri("/Resources/prendre_brosseadent.png", UriKind.Relative)); break; } break;
                case "mouillerBrosse": switch (order[1]) { case "texte":    i4.Source = new BitmapImage(new Uri("/Resources/mouillerBrosseText.png", UriKind.Relative)); break; case "image": i4.Source = new BitmapImage(new Uri("/Resources/mouiller_brosse.jpg", UriKind.Relative)); break; } break;
                case "mettreDentifrice": switch (order[1]) { case "texte": i3.Source = new BitmapImage(new Uri("/Resources/mettreDentifriceText.png", UriKind.Relative)); break; case "image": i3.Source = new BitmapImage(new Uri("/Resources/mettre_dentifrice.png", UriKind.Relative)); break; } break;
                case "rincer": switch (order[1]) { case "texte": i.Source = new BitmapImage(new Uri("/Resources/rincerText.png", UriKind.Relative)); break; case "image": i.Source = new BitmapImage(new Uri("/Resources/rincer_bouche.png", UriKind.Relative)); break; } break;
                case "cracher": switch (order[1]) { case "texte": i2.Source = new BitmapImage(new Uri("/Resources/cracherText.png", UriKind.Relative)); break; case "image": i2.Source = new BitmapImage(new Uri("/Resources/cracher.jpg", UriKind.Relative)); break; } break;
                case "brosser": switch (order[1]) { case "texte": i5.Source = new BitmapImage(new Uri("/Resources/brosserText.png", UriKind.Relative)); break; case "image": i2.Source = new BitmapImage(new Uri("/Resources/brosser.jpg", UriKind.Relative)); break; } break;

    }   
            }));
        }

        private void actionTexte()
        {
            i.Source = new BitmapImage(new Uri("/Resources/rincerText.png", UriKind.Relative));
            i2.Source = new BitmapImage(new Uri("/Resources/cracherText.png", UriKind.Relative));
            i3.Source = new BitmapImage(new Uri("/Resources/mettreDentifriceText.png", UriKind.Relative));
            i4.Source = new BitmapImage(new Uri("/Resources/mouillerBrosseText.png", UriKind.Relative));
            i5.Source = new BitmapImage(new Uri("/Resources/brosserText.png", UriKind.Relative));
            i6.Source = new BitmapImage(new Uri("/Resources/prendreBrosseText.png", UriKind.Relative));
        }
        private void actionImage()
        {
            i.Source = new BitmapImage(new Uri("/Resources/rincer_bouche.png", UriKind.Relative));
            i2.Source = new BitmapImage(new Uri("/Resources/cracher.jpg", UriKind.Relative));
            i3.Source = new BitmapImage(new Uri("/Resources/mettre_dentifrice.png", UriKind.Relative));
            i4.Source = new BitmapImage(new Uri("/Resources/mouiller_brosse.jpg", UriKind.Relative));
            i5.Source = new BitmapImage(new Uri("/Resources/brosser.jpg", UriKind.Relative));
            i6.Source = new BitmapImage(new Uri("/Resources/prendre_brosseadent.png", UriKind.Relative));
        }

        public string getFrise(int num) {

    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Frise));
    MemoryStream ms = new MemoryStream();
    if (num==0){
        js.WriteObject(ms, friseHautObjet);
        ms.Position = 0;
        StreamReader sr = new StreamReader(ms);
        string s = sr.ReadToEnd();
        Trace.WriteLine("JSON : " + s);
    }
    else {
             js.WriteObject(ms, friseBasObjet);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string s = sr.ReadToEnd();
            Trace.WriteLine("JSON : "+s);
    }

            return js.ToString();
    }
        private  void texteAide()
        {

            brosseDent.Source = new BitmapImage(new Uri("/Resources/brosserText.png", UriKind.Relative));
            dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative));
            verre.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative));
            brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosserText.png", UriKind.Relative));
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

        private async void animeMaison()
        {
            son.Stop();
            DoubleAnimation da = new DoubleAnimation();
            da.To = 1.2;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.AutoReverse = true;
            son.Open(new Uri(@"Resources\choixPiece.wav", UriKind.Relative));
            son.Play();
            await Task.Delay(3000);
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
            son.Stop();
            DoubleAnimation da = new DoubleAnimation();
            da.To = 1.2;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.AutoReverse = true;
            son.Open(new Uri(@"Resources\choixAtelier.wav", UriKind.Relative));
            son.Play();
            await Task.Delay(2000);
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


        private void valideObjet(){

            if (brosseadentBool && verreBool && dentifriceBool)
            {
                _sm.socket.Emit("changeView", 5);
                vueCourante++;
                aideTop.Visibility = Visibility.Hidden;
                aideBot.Visibility = Visibility.Hidden;
                ordonnancement.Visibility = Visibility.Visible;
                rotatefecheBas.Visibility = Visibility.Visible;
                rotatefecheHaut.Visibility = Visibility.Visible;
                son.Open(new Uri(@"Resources\placerActions.wav", UriKind.Relative));
                son.Play();
                ordonnacement = true;
                text.Text = "Ordonnancer les actions";
                text2.Text = "Ordonnancer les actions";
            }
        }
        private void rotateAllImage(int ind)
        {
            DoubleAnimation rotateAnimation=null;
            if (ind == 0)
            {
                Trace.WriteLine(" rotate 0");
                rotateAnimation = new DoubleAnimation(0, 180, TimeSpan.FromSeconds(1));
            }
            else
            {
                rotateAnimation = new DoubleAnimation(180,0, TimeSpan.FromSeconds(1));
            }
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
            i.SetValue(Canvas.TopProperty, p.Y-280);
            i.TouchDown += i_TouchDown;
            canvas.Children.Add(i);

            i2 = new Image();
            i2.Width = 200;
            i2.Height = 200;
            i2.Source = new BitmapImage(new Uri("/Resources/cracher.jpg", UriKind.Relative));
            i2.SetValue(Canvas.LeftProperty, p.X-110);
            i2.SetValue(Canvas.TopProperty, p.Y+80);
            i2.TouchDown += i2_TouchDown;
            canvas.Children.Add(i2);
        }

 
        private void createImageDentifrice(Point p)
        {
            i3 = new Image();
            i3.Width = 200;
            i3.Height = 200;
            i3.Source = new BitmapImage(new Uri("/Resources/mettre_dentifrice.png", UriKind.Relative));
            i3.SetValue(Canvas.LeftProperty, p.X - 110);
            i3.SetValue(Canvas.TopProperty, p.Y - 280);
            i3.TouchDown += i3_TouchDown;
            canvas.Children.Add(i3);

        }


        void i3_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                /*Trace.WriteLine("touch down img");
                TouchPoint p2 = e.GetTouchPoint(i3);
                Trace.WriteLine("Position touch down i3 = " + p2.Position);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i3.Width;
                imgTmp.Height = i3.Height;
                imgTmp.Source = i3.Source;
                if (!versLeBas)
                {
                    imgTmp.RenderTransformOrigin = new Point(0.5, 0.5);
                    imgTmp.RenderTransform = new RotateTransform(180);
                }
                else
                {
                    imgTmp.RenderTransform = new RotateTransform(0);
                }
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i3)-p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i3)-p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i3);
                canvas.Children.Add(imgTmp);
                touchDownImage = true;
                 * */
                image = 3;
                drop = true;
            }
            if (dernièreVue)
            {
                media1.Source = new Uri("Resources/videoAction3.wmv", UriKind.Relative);
                media2.Source = new Uri("Resources/videoAction3.wmv", UriKind.Relative);
                media1.Play();
                media2.Play();
            }
        }
        void i2_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                /*TouchPoint p2 = e.GetTouchPoint(i2);
                //Trace.WriteLine("Position touch down i2 = " + p2.Position);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i2.Width;
                imgTmp.Height = i2.Height;
                imgTmp.Source = i2.Source;
                if (!versLeBas)
                {
                    imgTmp.RenderTransformOrigin = new Point(0.5, 0.5);
                    imgTmp.RenderTransform = new RotateTransform(180);
                }
                else
                {
                    imgTmp.RenderTransform = new RotateTransform(0);
                }
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i2) - p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i2) - p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i2);
                canvas.Children.Add(imgTmp);
                 * */
                touchDownImage = true;
                image = 2;
                drop = true;
            }
            if (dernièreVue)
            {
                media1.Source = new Uri("Resources/videoAction5.wmv", UriKind.Relative);
                media2.Source = new Uri("Resources/videoAction5.wmv", UriKind.Relative);
                media1.Play();
                media2.Play();
            }
        }
        void i_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                /*
                TouchPoint p2 = e.GetTouchPoint(i);
                //Trace.WriteLine("Position touch down i = " + p2.Position);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i.Width;
                imgTmp.Height = i.Height;
                imgTmp.Source = i.Source;
                if (!versLeBas)
                {
                    imgTmp.RenderTransformOrigin = new Point(0.5, 0.5);
                    imgTmp.RenderTransform = new RotateTransform(180);
                }
                else
                {
                    imgTmp.RenderTransform = new RotateTransform(0);
                }
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i) - p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i) - p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i);
                canvas.Children.Add(imgTmp);
                 * */
                touchDownImage = true;
                image = 1;
                drop = true;
            }
            if (dernièreVue)
            {
                media1.Source = new Uri("Resources/videoAction6.wmv", UriKind.Relative);
                media2.Source = new Uri("Resources/videoAction6.wmv", UriKind.Relative);
                media1.Play();
                media2.Play();
            }
        }

        void i4_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                /*
                TouchPoint p2 = e.GetTouchPoint(i4);
                //Trace.WriteLine("Position touch down i4 = " + p2.Position);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i4.Width;
                imgTmp.Height = i4.Height;
                imgTmp.Source = i4.Source;
                if (!versLeBas)
                {
                    imgTmp.RenderTransformOrigin = new Point(0.5, 0.5);
                    imgTmp.RenderTransform = new RotateTransform(180);
                }
                else
                {
                    imgTmp.RenderTransform = new RotateTransform(0);
                }
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i4) - p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i4) - p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i4);
                canvas.Children.Add(imgTmp);
                 * */
                touchDownImage = true;
                image = 4;
                drop = true;
            }
            if (dernièreVue)
            {

                media1.Source=new Uri("Resources/videoAction2.wmv", UriKind.Relative);
                media2.Source = new Uri("Resources/videoAction2.wmv", UriKind.Relative);
                media1.Play();
                media2.Play();
            }

        }
        void i5_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                //TouchPoint p2 = e.GetTouchPoint(i5);
                //imgTmp.Margin = new Thickness(50, 50, 50, 50);
                //imgTmp.Width = i5.Width;
                //imgTmp.Height = i5.Height;
                //imgTmp.Source = i5.Source;
                /*if (!versLeBas)
                {
                    imgTmp.RenderTransformOrigin = new Point(0.5, 0.5);
                    imgTmp.RenderTransform = new RotateTransform(180);
                }
                else
                {
                    imgTmp.RenderTransform = new RotateTransform(0);
                }
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i5) - p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i5) - p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i5);
                canvas.Children.Add(imgTmp);*/
                touchDownImage = true;
                image = 5;
                drop = true;
            }
            if (dernièreVue)
            {

                media1.Source = new Uri("Resources/videoAction4.wmv", UriKind.Relative);
                media2.Source = new Uri("Resources/videoAction4.wmv", UriKind.Relative);
                media2.Play();
                media2.Play();
            }
        }

        void i6_TouchDown(object sender, TouchEventArgs e)
        {
            if (ordonnacement && !simpleTouch)
            {
                simpleTouch = true;
                /*
                TouchPoint p2 = e.GetTouchPoint(i6);
                imgTmp.Margin = new Thickness(50, 50, 50, 50);
                imgTmp.Width = i6.Width;
                imgTmp.Height = i6.Height;
                imgTmp.Source = i6.Source;
                if (!versLeBas)
                {
                    imgTmp.RenderTransformOrigin = new Point(0.5, 0.5);
                    imgTmp.RenderTransform = new RotateTransform(180);
                }
                else
                {
                    imgTmp.RenderTransform = new RotateTransform(0);
                }
                TouchPoint p = e.GetTouchPoint(this.canvas);
                imgTmp.SetValue(Canvas.LeftProperty, Canvas.GetLeft(i6) - p2.Position.X);
                imgTmp.SetValue(Canvas.TopProperty, Canvas.GetTop(i6) - p2.Position.Y);
                imgTmp.TouchMove += imgTmp_TouchMove;
                canvas.Children.Remove(i6);
                canvas.Children.Add(imgTmp);
                 * */
                touchDownImage = true;
                image = 6;
                drop = true;
            }
            if (dernièreVue)
            {

                media1.Source = new Uri("Resources/videoAction1.wmv", UriKind.Relative);
                media2.Source = new Uri("Resources/videoAction1.wmv", UriKind.Relative);
                media1.Play();
                media2.Play();
            }

        }
        private bool ordonnacement = false;
       /* private void imgTmp_TouchMove(object sender, TouchEventArgs e)
        
        {
            if (ordonnacement)
            {
                if (e.TouchDevice.GetIsFingerRecognized() )
                {
                    TouchPoint p = e.GetTouchPoint(this.canvas);
                    //Trace.WriteLine(p.Position);
                    imgTmp.SetValue(Canvas.LeftProperty, p.Position.X - 40);
                    imgTmp.SetValue(Canvas.TopProperty, p.Position.Y - 40);
                }
            }
        }*/
       
        private void createImageBrosse(Point p)
        {
            i4 = new Image();
            i4.Width = 200;
            i4.Height = 200;
            i4.Source = new BitmapImage(new Uri("/Resources/mouiller_brosse.jpg", UriKind.Relative));
            i4.SetValue(Canvas.LeftProperty, p.X +110);
            i4.SetValue(Canvas.TopProperty, p.Y - 280);
            i4.TouchDown += i4_TouchDown;
            canvas.Children.Add(i4);

            i5 = new Image();
            i5.Width = 200;
            i5.Height = 200;
            i5.Source = new BitmapImage(new Uri("/Resources/brosser.jpg", UriKind.Relative));
            i5.SetValue(Canvas.LeftProperty, p.X - 280);
            i5.SetValue(Canvas.TopProperty, p.Y - 280);
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

        private async void createCircleBrosse(Point p)
        {
            Ellipse myEllipse = new Ellipse();
            myEllipse.StrokeThickness = 4;
            myEllipse.Stroke = Brushes.LightGreen;
            myEllipse.Width = 200;
            myEllipse.Height = 200;
            myEllipse.SetValue(Canvas.LeftProperty, p.X -100);
            myEllipse.SetValue(Canvas.TopProperty, p.Y-100);
            canvas.Children.Add(myEllipse);
            DoubleAnimation da = new DoubleAnimation();
            da.To = 1.2;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.AutoReverse = true;

            ScaleTransform trans6 = new ScaleTransform();
            myEllipse.RenderTransform = trans6;
            myEllipse.RenderTransformOrigin = new Point(0.5, 0.5);
            trans6.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            trans6.BeginAnimation(ScaleTransform.ScaleYProperty, da);

            await Task.Delay(2000);
            canvas.Children.Remove(myEllipse);
        }

        private async void createCircleDentifrice(Point p)
        {
            Ellipse myEllipse2 = new Ellipse();
            myEllipse2.StrokeThickness = 4;
            myEllipse2.Stroke = Brushes.LightGreen;
            myEllipse2.Width = 200;
            myEllipse2.Height = 200;
            myEllipse2.SetValue(Canvas.LeftProperty, p.X - 100);
            myEllipse2.SetValue(Canvas.TopProperty, p.Y - 100);
            canvas.Children.Add(myEllipse2);
            DoubleAnimation da = new DoubleAnimation();
            da.To = 1.2;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.AutoReverse = true;

            ScaleTransform trans6 = new ScaleTransform();
            myEllipse2.RenderTransform = trans6;
            myEllipse2.RenderTransformOrigin = new Point(0.5, 0.5);
            trans6.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            trans6.BeginAnimation(ScaleTransform.ScaleYProperty, da);

            await Task.Delay(2000);
            canvas.Children.Remove(myEllipse2);
        }

        private async void createCircleVerre(Point p)
        {
            Ellipse myEllipse3 = new Ellipse();
            myEllipse3.StrokeThickness = 4;
            myEllipse3.Stroke = Brushes.LightGreen;
            myEllipse3.Width = 200;
            myEllipse3.Height = 200;
            myEllipse3.SetValue(Canvas.LeftProperty, p.X - 100);
            myEllipse3.SetValue(Canvas.TopProperty, p.Y - 100);
            canvas.Children.Add(myEllipse3);
            DoubleAnimation da = new DoubleAnimation();
            da.To = 1.2;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.AutoReverse = true;

            ScaleTransform trans6 = new ScaleTransform();
            myEllipse3.RenderTransform = trans6;
            myEllipse3.RenderTransformOrigin = new Point(0.5, 0.5);
            trans6.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            trans6.BeginAnimation(ScaleTransform.ScaleYProperty, da);

            await Task.Delay(2000);
            canvas.Children.Remove(myEllipse3);
        }

        private bool aideBool = false;
        private Point brosseObjetPoint, dentifriceObjetPoint, verreObjetPoint;
        private void OnVisualizationAdded(object sender, TagVisualizerEventArgs e)
        {

                Point p = e.TagVisualization.Center;
                Point t = new Point(p.X, p.Y + 210);
                try
                {
                    switch (e.TagVisualization.VisualizedTag.Value)
                    {
                        case 0x31: brosseObjetPoint = t; createImageBrosse(t); if (aideBool || aideBrosse) { Trace.WriteLine("BOOL =" + aideBool + " " + aideBrosse); borderAideBrosseDent.BorderBrush = Brushes.LightGreen; borderAideBrosseDent2.BorderBrush = Brushes.LightGreen; } brosseadentBool = true; valideObjet(); break;
                        case 0x05: dentifriceObjetPoint = t; createImageDentifrice(t); if (aideBool || aideDentifrice) { borderDentifrice.BorderBrush = Brushes.LightGreen; borderDentifrice2.BorderBrush = Brushes.LightGreen; } borderDentifrice.Visibility = Visibility.Visible; dentifriceBool = true; valideObjet(); break;
                        case 0x24: verreObjetPoint = t; createImageVerre(t); if (aideBool || aideVerre) { borderVerre.BorderBrush = Brushes.LightGreen; borderVerre2.BorderBrush = Brushes.LightGreen; } borderVerre.Visibility = Visibility.Visible; verreBool = true; valideObjet(); break;
                        default: break;
                    }
                }
                catch (System.Exception ex) { Trace.WriteLine("exeption " + ex); }
            
        }



        void Window1_Closing(object sender, CancelEventArgs e)
        {

            Application.Current.Shutdown();

        }

        private void touch(object sender, TouchEventArgs e)
        {
            _sm.socket.Emit("changeView", 2);
            myGrid.Visibility = Visibility.Hidden;
      
           maison.Visibility = Visibility.Visible;
           animeMaison();
        }

        private  void brosseadent_Touch(object sender, TouchEventArgs e)
        {
            son.Stop();
            _sm.socket.Emit("changeView", 4);
            vueCourante++;
            objetVue();
           
        }

        private async void objetVue()
        {
            son.Stop();

            atelier.Visibility = Visibility.Hidden;
            objet.Visibility = Visibility.Visible;
            son.Open(new Uri(@"Resources\poserObjetBrossageDents.wav", UriKind.Relative));
            son.Play();
            //Objet en Texte
            await Task.Delay(3000);
            brosseDent.Source = new BitmapImage(new Uri("/Resources/brosserText.png", UriKind.Relative));
            dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative));
            verre.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative));
            brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosserText.png", UriKind.Relative));
            dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice.png", UriKind.Relative));
            verre2.Source = new BitmapImage(new Uri("/Resources/verre.png", UriKind.Relative));
            aideBool = true;
            //Objet en Image
            await Task.Delay(3000);
            brosseDent.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative));
            dentifrice.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative));
            verre.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative));
            brosseDent2.Source = new BitmapImage(new Uri("/Resources/brosse_grandT.png", UriKind.Relative));
            dentifrice2.Source = new BitmapImage(new Uri("/Resources/dentifrice_grand.png", UriKind.Relative));
            verre2.Source = new BitmapImage(new Uri("/Resources/verre_grand.png", UriKind.Relative));
        }
        private void salledebain_Touch(object sender, TouchEventArgs e)
        {
            son.Stop();
            _sm.socket.Emit("changeView", 3);
            vueCourante++;
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
            if (!dernièreVue)
            {
                Point p = e.TagVisualization.Center;
                Point t = new Point(p.X, p.Y + 210);
                try
                {
                    switch (e.TagVisualization.VisualizedTag.Value)
                    {
                        case 0x31: deleteImageBrosse(t); borderAideBrosseDent.BorderBrush = Brushes.Transparent; borderAideBrosseDent2.BorderBrush = Brushes.Transparent; brosseadentBool = false; break;
                        case 0x05: deleteImageDentifrice(t); borderDentifrice.BorderBrush = Brushes.Transparent; borderDentifrice2.BorderBrush = Brushes.Transparent; dentifriceBool = false; break;
                        case 0x24: deleteImageVerre(t); borderVerre.BorderBrush = Brushes.Transparent; borderVerre2.BorderBrush = Brushes.Transparent; verreBool = false; break;
                        default: break;
                    }
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("exeption " + ex);
                }
            }
        }
        private void moveImageVerre(Point p)
        {
            i.SetValue(Canvas.LeftProperty, p.X - 110);
            i.SetValue(Canvas.TopProperty, p.Y - 280);
            i2.SetValue(Canvas.LeftProperty, p.X - 110);
            i2.SetValue(Canvas.TopProperty, p.Y + 80);
        }

        private void moveImageDentifrice(Point p)
        {
            i3.SetValue(Canvas.LeftProperty, p.X - 110);
            i3.SetValue(Canvas.TopProperty, p.Y - 280);
        }

        private void moveImageBrosse(Point p)
        {
            i4.SetValue(Canvas.LeftProperty, p.X + 110);
            i4.SetValue(Canvas.TopProperty, p.Y - 280);
            i5.SetValue(Canvas.LeftProperty, p.X - 280);
            i5.SetValue(Canvas.TopProperty, p.Y - 280);
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
                    case 1: if ((img.Name == "bloc1" && h1) || (img.Name == "bloc2" && h2) || (img.Name == "bloc3" && h3) || (img.Name == "bloc4" && h4) || (img.Name == "bloc5" && h5) || (img.Name == "bloc6" && h6) || (img.Name == "bloc1B" && b1) || (img.Name == "bloc2B" && b2) || (img.Name == "bloc3B" && b3) || (img.Name == "bloc4B" && b4) || (img.Name == "bloc5B" && b5) || (img.Name == "bloc6B" && b6)) { break; } if (img.Name == "bloc1" || img.Name == "bloc5" || img.Name == "bloc4" || img.Name == "bloc2" || img.Name == "bloc3" || img.Name == "bloc6") { img.RenderTransformOrigin = new Point(0.5, 0.5); img.RenderTransform = new RotateTransform(180); } img.Source = new BitmapImage(new Uri("/Resources/rincer_bouche.png", UriKind.Relative)); break;//canvas.Children.Add(i); break;
                    case 2: if ((img.Name == "bloc1" && h1) || (img.Name == "bloc2" && h2) || (img.Name == "bloc3" && h3) || (img.Name == "bloc4" && h4) || (img.Name == "bloc5" && h5) || (img.Name == "bloc6" && h6) || (img.Name == "bloc1B" && b1) || (img.Name == "bloc2B" && b2) || (img.Name == "bloc3B" && b3) || (img.Name == "bloc4B" && b4) || (img.Name == "bloc5B" && b5) || (img.Name == "bloc6B" && b6)) { break; } if (img.Name == "bloc1" || img.Name == "bloc5" || img.Name == "bloc4" || img.Name == "bloc2" || img.Name == "bloc3" || img.Name == "bloc6") { img.RenderTransformOrigin = new Point(0.5, 0.5); img.RenderTransform = new RotateTransform(180); } img.Source = new BitmapImage(new Uri("/Resources/cracher.jpg", UriKind.Relative)); break;// canvas.Children.Add(i2); break;
                    case 3: if ((img.Name == "bloc1" && h1) || (img.Name == "bloc2" && h2) || (img.Name == "bloc3" && h3) || (img.Name == "bloc4" && h4) || (img.Name == "bloc5" && h5) || (img.Name == "bloc6" && h6) || (img.Name == "bloc1B" && b1) || (img.Name == "bloc2B" && b2) || (img.Name == "bloc3B" && b3) || (img.Name == "bloc4B" && b4) || (img.Name == "bloc5B" && b5) || (img.Name == "bloc6B" && b6)) { break; } if (img.Name == "bloc1" || img.Name == "bloc5" || img.Name == "bloc4" || img.Name == "bloc2" || img.Name == "bloc3" || img.Name == "bloc6") { img.RenderTransformOrigin = new Point(0.5, 0.5); img.RenderTransform = new RotateTransform(180); } img.Source = new BitmapImage(new Uri("/Resources/mettre_dentifrice.png", UriKind.Relative)); break;// canvas.Children.Add(i3); break;
                    case 4: if ((img.Name == "bloc1" && h1) || (img.Name == "bloc2" && h2) || (img.Name == "bloc3" && h3) || (img.Name == "bloc4" && h4) || (img.Name == "bloc5" && h5) || (img.Name == "bloc6" && h6) || (img.Name == "bloc1B" && b1) || (img.Name == "bloc2B" && b2) || (img.Name == "bloc3B" && b3) || (img.Name == "bloc4B" && b4) || (img.Name == "bloc5B" && b5) || (img.Name == "bloc6B" && b6)) { break; } if (img.Name == "bloc1" || img.Name == "bloc5" || img.Name == "bloc4" || img.Name == "bloc2" || img.Name == "bloc3" || img.Name == "bloc6") { img.RenderTransformOrigin = new Point(0.5, 0.5); img.RenderTransform = new RotateTransform(180); } img.Source = new BitmapImage(new Uri("/Resources/mouiller_brosse.jpg", UriKind.Relative)); break;// canvas.Children.Add(i4); break;
                    case 5: if ((img.Name == "bloc1" && h1) || (img.Name == "bloc2" && h2) || (img.Name == "bloc3" && h3) || (img.Name == "bloc4" && h4) || (img.Name == "bloc5" && h5) || (img.Name == "bloc6" && h6) || (img.Name == "bloc1B" && b1) || (img.Name == "bloc2B" && b2) || (img.Name == "bloc3B" && b3) || (img.Name == "bloc4B" && b4) || (img.Name == "bloc5B" && b5) || (img.Name == "bloc6B" && b6)) { break; } if (img.Name == "bloc1" || img.Name == "bloc5" || img.Name == "bloc4" || img.Name == "bloc2" || img.Name == "bloc3" || img.Name == "bloc6") { img.RenderTransformOrigin = new Point(0.5, 0.5); img.RenderTransform = new RotateTransform(180); } img.Source = new BitmapImage(new Uri("/Resources/brosser.jpg", UriKind.Relative)); break;// canvas.Children.Add(i5); break;
                    case 6: if ((img.Name == "bloc1" && h1) || (img.Name == "bloc2" && h2) || (img.Name == "bloc3" && h3) || (img.Name == "bloc4" && h4) || (img.Name == "bloc5" && h5) || (img.Name == "bloc6" && h6) || (img.Name == "bloc1B" && b1) || (img.Name == "bloc2B" && b2) || (img.Name == "bloc3B" && b3) || (img.Name == "bloc4B" && b4) || (img.Name == "bloc5B" && b5) || (img.Name == "bloc6B" && b6)) { break; } if (img.Name == "bloc1" || img.Name == "bloc5" || img.Name == "bloc4" || img.Name == "bloc2" || img.Name == "bloc3" || img.Name == "bloc6") { img.RenderTransformOrigin = new Point(0.5, 0.5); img.RenderTransform = new RotateTransform(180); } img.Source = new BitmapImage(new Uri("/Resources/prendre_brossedent.png", UriKind.Relative)); break;// canvas.Children.Add(i6); break;
                    default: break;
            }
                entreOrdre(img);
                //canvas.Children.Remove(imgTmp);
                drop = false;
                touchSurFrise = true;
                touchDownImage = false;
            }
        }

        private void entreOrdre(Image img)
        {
            switch (img.Name)
            {
                case "bloc1": orderFriseHaut[0] = image; if (image == trueOrder[5])
                    {
                       if(imageBloc1){friseHautObjet.atelier="image";} else{friseHautObjet.atelier="text";}  borderbloc1.BorderBrush = Brushes.LightGreen; h1 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play();} break;
                case "bloc2": if (imageBloc2) { friseHautObjet.bloc2 = "image"; } else { friseHautObjet.bloc2 = "text"; } orderFriseHaut[1] = image; if (image == trueOrder[4]) { borderbloc2.BorderBrush = Brushes.LightGreen; h2 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
                case "bloc3": if (imageBloc3) { friseHautObjet.bloc3 = "image"; } else { friseHautObjet.bloc3 = "text"; } orderFriseHaut[2] = image; if (image == trueOrder[3]) { borderbloc3.BorderBrush = Brushes.LightGreen; h3 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
                case "bloc4": if (imageBloc4) { friseHautObjet.bloc4 = "image"; } else { friseHautObjet.bloc4 = "text"; } orderFriseHaut[3] = image; if (image == trueOrder[2]) { borderbloc4.BorderBrush = Brushes.LightGreen; h4 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
                case "bloc5": if (imageBloc5) { friseHautObjet.bloc5 = "image"; } else { friseHautObjet.bloc5 = "text"; } orderFriseHaut[4] = image; if (image == trueOrder[1]) { borderbloc5.BorderBrush = Brushes.LightGreen; h5 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
                case "bloc6": if (imageBloc6) { friseHautObjet.bloc6 = "image"; } else { friseHautObjet.bloc6 = "text"; } orderFriseHaut[5] = image; if (image == trueOrder[0]) { borderbloc6.BorderBrush = Brushes.LightGreen; h6 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
                case "bloc1B": if (imageBloc1B) { friseHautObjet.atelier = "image"; } else { friseHautObjet.atelier = "text"; } orderFriseBas[0] = image; if (image == trueOrder[0]) { borderbloc7.BorderBrush = Brushes.LightGreen; b1 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
                case "bloc2B": if (imageBloc2B) { friseHautObjet.bloc2 = "image"; } else { friseHautObjet.bloc2 = "text"; } orderFriseBas[1] = image; if (image == trueOrder[1]) { borderbloc8.BorderBrush = Brushes.LightGreen; b2 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
                case "bloc3B": if (imageBloc3B) { friseHautObjet.bloc3 = "image"; } else { friseHautObjet.bloc3 = "text"; } orderFriseBas[2] = image; if (image == trueOrder[2]) { borderbloc9.BorderBrush = Brushes.LightGreen; b3 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
                case "bloc4B": if (imageBloc4B) { friseHautObjet.bloc4 = "image"; } else { friseHautObjet.bloc4 = "text"; } orderFriseBas[3] = image; if (image == trueOrder[3]) { borderbloc10.BorderBrush = Brushes.LightGreen; b4 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
                case "bloc5B": if (imageBloc5B) { friseHautObjet.bloc5 = "image"; } else { friseHautObjet.bloc5 = "text"; } orderFriseBas[4] = image; if (image == trueOrder[4]) { borderbloc11.BorderBrush = Brushes.LightGreen; b5 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
                case "bloc6B": if (imageBloc6B) { friseHautObjet.bloc6 = "image"; } else { friseHautObjet.bloc6 = "text"; } orderFriseBas[5] = image; if (image == trueOrder[5]) { borderbloc12.BorderBrush = Brushes.LightGreen; b6 = true; son.Open(new Uri(@"Resources\bravo.mp3", UriKind.Relative)); son.Play(); } break;
            }
            testOrdre();
        }

        private bool ordreFriseHaut = false, ordreFriseBas = false;
        private bool dernièreVue = false;
        private void testOrdre()
        {
            ordreFriseBas = true;
            ordreFriseHaut = true;
            int i = 0;
            for (i = 0; i < 6; i++)
            {
                if (trueOrder[5-i] != orderFriseHaut[i])
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
            if (ordreFriseHaut && ordreFriseBas)
            {
                //son bravo
                // vue vidéo
                _sm.socket.Emit("changeView", 6);
                vueCourante++;
                dernièreVue = true;
                ordonnacement = false;
                ordonnancement.Visibility = Visibility.Visible;
               
                videoHaut.Visibility = Visibility.Visible;
                videoBas.Visibility = Visibility.Visible;
                friseBas.Visibility = Visibility.Hidden;
                friseHaut.Visibility = Visibility.Hidden;
                text.Text = "Touchez une image pour lancer la vidéo";
                text2.Text = "Touchez une image pour lancer la vidéo";
                Trace.WriteLine("SUCCES BRAVO");
                son.Open(new Uri(@"Resources\selectionnerImagePourVideo.wav", UriKind.Relative));
                son.Play();
            }
        }
        private void mouveDelete(object sender, TouchEventArgs e)
        {
            if (!drop)
            {
                touchSurFrise = true;
                Image img = sender as Image;
                switch (img.Name)
                {
                    case "bloc1": if(!h1) img.Source = new BitmapImage(new Uri("/Resources/cubeTop.png", UriKind.Relative)); break;
                    case "bloc2": if (!h2) img.Source = new BitmapImage(new Uri("/Resources/cubeTop.png", UriKind.Relative)); break;
                    case "bloc3": if (!h3) img.Source = new BitmapImage(new Uri("/Resources/cubeTop.png", UriKind.Relative)); break;
                    case "bloc4": if (!h4) img.Source = new BitmapImage(new Uri("/Resources/cubeTop.png", UriKind.Relative)); break;
                    case "bloc5": if (!h5) img.Source = new BitmapImage(new Uri("/Resources/cubeTop.png", UriKind.Relative)); break;
                    case "bloc6": if (!h6) img.Source = new BitmapImage(new Uri("/Resources/cubeTopCoin.png", UriKind.Relative)); break;
                    case "bloc1B": if (!b1) img.Source = new BitmapImage(new Uri("/Resources/cubeBotCoin.png", UriKind.Relative)); break;
                    case "bloc2B": if (!b2) img.Source = new BitmapImage(new Uri("/Resources/cubeBot.png", UriKind.Relative)); break;
                    case "bloc3B": if (!b3) img.Source = new BitmapImage(new Uri("/Resources/cubeBot.png", UriKind.Relative)); break;
                    case "bloc4B": if (!b4) img.Source = new BitmapImage(new Uri("/Resources/cubeBot.png", UriKind.Relative)); break;
                    case "bloc5B": if (!b5) img.Source = new BitmapImage(new Uri("/Resources/cubeBot.png", UriKind.Relative)); break;
                    case "bloc6B": if (!b6) img.Source = new BitmapImage(new Uri("/Resources/cubeBot.png", UriKind.Relative)); break;
                }
                

            }
        }
        private bool h1, h2, h3, h4, h5, h6, b1, b2, b3, b4, b5, b6;
 /* private void imgTmp_TouchUp(object sender, TouchEventArgs e)
  {
      if (e.TouchDevice.GetIsFingerRecognized())
      {
          simpleTouch = false;
          drop = false;
          if (ordonnacement && !touchSurFrise && touchDownImage)
          {
              touchDownImage = false;
              switch (image)
              {
                  case 1: canvas.Children.Add(i); break;
                  case 2: canvas.Children.Add(i2); break;
                  case 3: canvas.Children.Add(i3); break;
                  case 4: canvas.Children.Add(i4); break;
                  case 5: canvas.Children.Add(i5); break;
                  case 6: canvas.Children.Add(i6); break;
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
        */
  private void OnvisualMoved(object sender, TagVisualizerEventArgs e)
  {

      Point p = e.TagVisualization.Center;

      Point t = new Point(p.X, p.Y + 210);

      try
      {

          switch (e.TagVisualization.VisualizedTag.Value)
          {

              case 0x31: moveImageBrosse(t);  break;

              case 0x05: moveImageDentifrice(t); break;

              case 0x24: moveImageVerre(t); break;

              default: break;

          }

      }

      catch (System.Exception ex)
      {

          Trace.WriteLine("exeption " + ex);

      }

  }

  private bool versLeBas = true;
  private void rotateHaut(object sender, TouchEventArgs e)
  {
      if (versLeBas)
      {
          versLeBas = false;
          rotateAllImage(0);
      }
  }

  private void rotateBas(object sender, TouchEventArgs e)
  {
      if (!versLeBas)
      {
          versLeBas = true;
          rotateAllImage(1);
      }
  }

    }
}


