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
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows.Media.Animation;

namespace SurfacePaint
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {

        public static Color _currentColor;

        private static SurfaceWindow1 instance = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            if (instance == null)
            {
                instance = this;
            }

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();

            //Populate LibraryBar
            LoadLibraryBarContent();

            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 360;
            da.Duration = new Duration(TimeSpan.FromSeconds(5));
            da.RepeatBehavior = RepeatBehavior.Forever;
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, da);
        }

        /// <summary>
        /// Singleton pattern
        /// </summary>
        /// <returns></returns>
        public static SurfaceWindow1 GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

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

        /// <summary>
        /// Called when the user click on a menu element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuClick(object sender, RoutedEventArgs e)
        {
            // Get the clicked element menu
            ElementMenuItem item = e.OriginalSource as ElementMenuItem;

            if (item.Name.Equals("ModeDraw"))
            {
                //Change InkCanvas editing mode
                InkCanvas.EditingMode = SurfaceInkEditingMode.Ink;

                //Change image on the menu (a pen)
                Menu.Background = Resources["icon_crayon"] as ImageBrush;

                //Change the text on the menu (draw)
                MenuMode.Source = (Resources["icon_draw"] as ImageBrush).ImageSource;

                Sounds.Play(EnumSound.CRAYON);
            }
            else if(item.Name.Equals("ModeErase"))
            {
                //Change InkCanvas editing mode
                InkCanvas.EditingMode = SurfaceInkEditingMode.EraseByStroke;

                //Change image on the menu (an eraser)
                Menu.Background = Resources["icon_eraser"] as ImageBrush;

                //Change the text on the menu (draw)
                MenuMode.Source = (Resources["icon_erase"] as ImageBrush).ImageSource;

                Sounds.Play(EnumSound.GOMME);
            }
        }

        System.Collections.ObjectModel.ObservableCollection<String> items;

        /// <summary>
        /// Create a list of url and set them as the library datasource
        /// </summary>
        private void LoadLibraryBarContent()
        {
            try
            {
                string publicFoldersPath = Environment.GetEnvironmentVariable("public");
                // These are default OS folders. 
                string publicImagesPath = publicFoldersPath + @"\Pictures\Sample Pictures";

                String[] files = System.IO.Directory.GetFiles(publicImagesPath, "*.jpg");

                items = new System.Collections.ObjectModel.ObservableCollection<String>();

                foreach (String file in files)
                {
                    items.Add(file);
                }

                library.ItemsSource = items;

            }
            catch (System.IO.DirectoryNotFoundException)
            {

            }
        }

        /// <summary>
        /// Called when the user drops a library element into the ScatterView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScatterViewDrop(object sender, SurfaceDragDropEventArgs e)
        {
            //Retrieve the index of the dragged object
            int position = items.IndexOf(e.Cursor.Data as String);

            //Set the background of the ScatterView
            ImageSource i = new BitmapImage(new Uri(e.Cursor.Data as String));
            scatter_main.Background = new ImageBrush(i);

            //Remove the item from the Library source and add it again (to be able to use it again)
            items.Remove(e.Cursor.Data as String);
            items.Insert(position, e.Cursor.Data as String);
           
        }

        /// <summary>
        /// Called when the user does a Hold gesture on the ScatterViewItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScatterViewItemHoldGesture(object sender, TouchEventArgs e)
        {
            //Change the color of the bottom ScatterViewItem with the current color from the palette
            myGrid.Background = new SolidColorBrush(_currentColor);
        }
    }
}