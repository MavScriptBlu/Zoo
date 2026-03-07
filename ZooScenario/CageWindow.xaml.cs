using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Animals;
using CagedItems;
using Utilities;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for CageWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class CageWindow : Window
    {
        /// <summary>
        /// The cage to display.
        /// </summary>
        private Cage cage;

        /// <summary>
        /// The timer that controls cage redrawing.
        /// </summary>
        private Timer redrawTimer;

        /// <summary>
        /// Initializes a new instance of the CageWindow class.
        /// </summary>
        /// <param name="cage">The cage to display.</param>
        public CageWindow(Cage cage)
        {
            this.cage = cage;
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets up the redraw timer when the window loads.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.redrawTimer = new Timer(100);
            this.redrawTimer.Elapsed += this.RedrawHandler;
            this.redrawTimer.Start();
        }

        /// <summary>
        /// Stops the redraw timer when the window is closing.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.redrawTimer != null)
            {
                this.redrawTimer.Stop();
            }
        }

        /// <summary>
        /// Handles the redraw timer elapsed event.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void RedrawHandler(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.DrawAllItems();
            }));
        }

        /// <summary>
        /// Draws all items in the cage.
        /// </summary>
        private void DrawAllItems()
        {
            this.animalCanvas.Children.Clear();

            foreach (ICageable item in this.cage.CagedItems)
            {
                this.DrawItem(item);
            }
        }

        /// <summary>
        /// Draws a single cageable item on the canvas.
        /// </summary>
        /// <param name="item">The item to draw.</param>
        private void DrawItem(ICageable item)
        {
            Viewbox viewbox = this.GetViewBox(item.ResourceKey, item.DisplaySize);

            if (viewbox != null)
            {
                Canvas.SetLeft(viewbox, item.XPosition);
                Canvas.SetTop(viewbox, item.YPosition);

                // Flip image if animal is moving left.
                if (item is Animal)
                {
                    Animal animal = item as Animal;
                    if (animal.XDirection == HorizontalDirection.Left)
                    {
                        viewbox.RenderTransformOrigin = new Point(0.5, 0.5);
                        viewbox.RenderTransform = new ScaleTransform(-1, 1);
                    }
                }

                this.animalCanvas.Children.Add(viewbox);
            }
        }

        /// <summary>
        /// Gets a viewbox containing the resource image for the specified key.
        /// </summary>
        /// <param name="resourceKey">The resource key to look up.</param>
        /// <param name="displaySize">The display size scale factor.</param>
        /// <returns>A viewbox containing the resource, or null if not found.</returns>
        private Viewbox GetViewBox(string resourceKey, double displaySize)
        {
            Canvas resourceCanvas = Application.Current.Resources[resourceKey] as Canvas;

            if (resourceCanvas == null)
            {
                return null;
            }

            // Clone the canvas using XAML serialization.
            string xaml = XamlWriter.Save(resourceCanvas);
            Canvas clonedCanvas = XamlReader.Parse(xaml) as Canvas;

            Viewbox viewbox = new Viewbox();
            viewbox.Child = clonedCanvas;
            viewbox.Width = 100 * displaySize;
            viewbox.Height = 100 * displaySize;

            return viewbox;
        }
    }
}
