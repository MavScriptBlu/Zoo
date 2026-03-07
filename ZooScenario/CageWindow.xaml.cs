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
            this.cageGrid.Children.Clear();

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
            Viewbox viewbox = this.GetViewBox(item.ResourceKey, item.DisplaySize, item.XPosition, item.YPosition);

            if (viewbox != null)
            {
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

                this.cageGrid.Children.Add(viewbox);
            }
        }

        /// <summary>
        /// Gets a viewbox containing the resource image for the specified key.
        /// </summary>
        /// <param name="resourceKey">The resource key to look up.</param>
        /// <param name="displayScale">The display scale factor.</param>
        /// <param name="xPosition">The horizontal position of the item.</param>
        /// <param name="yPosition">The vertical position of the item.</param>
        /// <returns>A viewbox containing the resource, or null if not found.</returns>
        private Viewbox GetViewBox(string resourceKey, double displayScale, int xPosition, int yPosition)
        {
            Canvas canvas = Application.Current.Resources[resourceKey] as Canvas;

            if (canvas == null)
            {
                return null;
            }

            // Clone the canvas using XAML serialization.
            string xaml = XamlWriter.Save(canvas);
            Canvas clonedCanvas = XamlReader.Parse(xaml) as Canvas;

            Viewbox finishedViewBox = new Viewbox();
            double imageRatio = canvas.Width / canvas.Height;
            double itemWidth = this.cageGrid.ActualWidth * 0.2 * displayScale;
            double itemHeight = itemWidth / imageRatio;
            finishedViewBox.Width = itemWidth;
            finishedViewBox.Height = itemHeight;

            double xPercent = (this.cageGrid.ActualWidth - itemWidth) / 800;
            double yPercent = (this.cageGrid.ActualHeight - itemHeight) / 400;
            int posX = Convert.ToInt32(xPosition * xPercent);
            int posY = Convert.ToInt32(yPosition * yPercent);

            finishedViewBox.Margin = new Thickness(posX, posY, 0, 0);
            finishedViewBox.HorizontalAlignment = HorizontalAlignment.Left;
            finishedViewBox.VerticalAlignment = VerticalAlignment.Top;
            finishedViewBox.Child = clonedCanvas;

            return finishedViewBox;
        }
    }
}
