// <copyright file="GradeZoneSelection.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The interface for selecting the region of the panel that will contain the grade.</summary>
namespace DPXGrader
{
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
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for GradeZoneSelection.xaml
    /// </summary>
    public partial class GradeZoneSelection : Window
    {
        /// <summary>
        /// The size of the grade box.
        /// </summary>
        private int boxSize;

        /// <summary>
        /// The location of the grade box.
        /// </summary>
        private BoxLocation boxLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradeZoneSelection"/> class.
        /// </summary>
        public GradeZoneSelection()
        {
            InitializeComponent();
            this.boxSize = 50;
            this.boxLocation = BoxLocation.TopLeft;
        }

        /// <summary>
        /// The possible locations of the grade box
        /// </summary>
        private enum BoxLocation
        {
            /// <summary>
            /// Location represented by the top left corner.
            /// </summary>
            TopLeft,

            /// <summary>
            /// Location represented by the top right corner.
            /// </summary>
            TopRight,

            /// <summary>
            /// Location represented by the bottom left corner.
            /// </summary>
            BottomLeft,

            /// <summary>
            /// Location represented by the bottom right corner.
            /// </summary>
            BottomRight
        }

        /// <summary>
        /// Processes a change in the location of the box.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void RadioLocationChanged(object sender, RoutedEventArgs e)
        {
            if (this.RadioTopLeft.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.TopLeft;
            }
            else if (this.RadioTopRight.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.TopRight;
            }
            else if (this.RadioBottomLeft.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.BottomLeft;
            }
            else if (this.RadioBottomRight.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.BottomRight;
            }

            // Redraw the rectangle based on the new settings
            this.RedrawRectangle();
        }

        /// <summary>
        /// Sliders the value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedPropertyChangedEventArgs&lt;System.Double&gt;"/> instance containing the event data.</param>
        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.boxSize = (int)this.BoxSizeSlider.Value;

            // Redraw the rectangle based on the new settings
            this.RedrawRectangle();
        }

        /// <summary>
        /// Redraws the rectangle on the canvas based on the new location and size.
        /// </summary>
        private void RedrawRectangle()
        {
            // Set the size of the box
            this.Boxy.Width = this.boxSize;
            this.Boxy.Height = this.boxSize;

            // Set the relative position of the box
            if (this.boxLocation.Equals(BoxLocation.TopLeft))
            {
                Canvas.SetLeft(this.Boxy, 0);
                Canvas.SetTop(this.Boxy, 0);
            }
            else if (this.boxLocation.Equals(BoxLocation.TopRight))
            {
                Canvas.SetLeft(this.Boxy, this.Inky.Width - this.boxSize);
                Canvas.SetTop(this.Boxy, 0);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomLeft))
            {
                Canvas.SetLeft(this.Boxy, 0);
                Canvas.SetTop(this.Boxy, this.Inky.Height - this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomRight))
            {
                Canvas.SetLeft(this.Boxy, this.Inky.Width - this.boxSize);
                Canvas.SetTop(this.Boxy, this.Inky.Height - this.boxSize);
            }
        }
    }
}
